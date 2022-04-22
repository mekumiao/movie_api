using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Model;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 用户搜索历史
/// </summary>
public class UserSearchHistoryController : MyBaseController
{
    private readonly MovieDbContext _dbContext;
    private readonly IUser _user;

    public UserSearchHistoryController(MovieDbContext dbContext, IUser user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="tag">有效值(Movie)</param>
    /// <param name="q"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <param name="sorting">排序</param>
    /// <param name="hasTotal"></param>
    /// <param name="all">查询全部所有用户的历史(仅管理员生效)。default is false</param>
    /// <param name="distinct">去除重复的记录值</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<UserSearchHistoryDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<UserSearchHistoryDto>> ListAsync(
        [FromServices] MovieDbContext dbContext,
        [FromQuery, StringColumn] string? tag,
        [FromQuery, StringColumn] string? q,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, RemarkColumn] string? sorting,
        [FromQuery] bool? hasTotal,
        [FromQuery] bool? all,
        [FromQuery] bool? distinct)
    {
        var histories = dbContext.UserSearchHistories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(tag))
        {
            histories = histories.Where(x => x.Tag == tag);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            histories = histories.Where(x => x.Value.Contains(q));
        }

        if (!(all is true && _user.IsAdmin))
        {
            histories = histories.Where(x => x.UserId == _user.Id);
        }

        if (distinct is true)
        {
            // 去掉重复的搜索记录
            histories = (from t1 in histories
                         join t2 in (from it1 in histories
                                     group it1 by new { it1.Tag, it1.Value } into gp
                                     select new
                                     {
                                         Id = gp.Max(x => x.Id)
                                     }) on t1.Id equals t2.Id
                         select t1);
        }

        var query = histories.Select(UserSearchHistoryMapper.ProjectToDto);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.Id);
        }

        return await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="tag">有效值(Movie)</param>
    /// <param name="value"></param>
    /// <param name="all"></param>
    /// <returns></returns>
    [HttpDelete("{tag}")]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync(
        [FromRoute, StringColumn] string tag,
        [FromQuery, StringColumn] string? value,
        [FromQuery] bool? all)
    {
        var query = _dbContext.UserSearchHistories.Where(x => x.Tag == tag);

        if (value is not null)
        {
            query = query.Where(x => x.Value == value);
        }

        if (!(all is true && _user.IsAdmin))
        {
            query = query.Where(x => x.UserId == _user.Id);
        }

        var items = await query.ToListAsync();
        if (items.Any())
        {
            _dbContext.UserSearchHistories.RemoveRange(items);
            return await _dbContext.SaveChangesAsync();
        }
        return default;
    }
}
