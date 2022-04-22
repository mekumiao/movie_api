using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.Events;
using MovieAPI.Model;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 电影文件
/// </summary>
[Authorize(Roles = MyConst.Role.Admin)]
public class MovieFileController : MyBaseController
{
    private readonly MovieDbContext _dbContext;

    public MovieFileController(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OutputResult<MovieFileDto>), StatusCodes.Status200OK)]
    public async Task<MovieFileDto?> GetAsync([FromRoute] long id)
    {
        var item = await _dbContext.MovieFiles.Include(x => x.Movies)
                                              .Select(MovieFileMapper.ProjectToDto)
                                              .SingleOrDefaultAsync(x => x.Id == id);
        SchemeHostAdaptTo(item);
        return item;
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<MovieFileDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<MovieFileDto>> ListAsync(
        [FromQuery] bool? hasTotal,
        [FromQuery, StringColumn] string? q,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? sorting,
        [FromServices] IEventBus eventBus)
    {
        var movieFiles = _dbContext.MovieFiles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            movieFiles = movieFiles.Where(x => x.DiskURL.Contains(q))
                                   .Where(x => x.FileName.Contains(q))
                                   .Where(x => x.FileFullName.Contains(q));
            eventBus.RaiseEvent<UserSearchEvent>(new UserSearchEvent("MovieFile", q));
        }

        var query = movieFiles.Select(MovieFileMapper.ProjectToDto);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.Id);
        }
        var items = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);
        SchemeHostAdaptToRange(items);
        return items;
    }
}
