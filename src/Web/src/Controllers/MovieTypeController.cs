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
/// 电影分类
/// </summary>
public class MovieTypeController : MyBaseController
{
    private readonly MovieDbContext _dbContext;

    public MovieTypeController(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OutputResult<MovieTypeDto>), StatusCodes.Status200OK)]
    public async Task<MovieTypeDto?> GetAsync([FromRoute] long id)
    {
        var item = await _dbContext.MovieTypes.AsNoTracking()
                                              .Select(MovieTypeMapper.ProjectToDto)
                                              .SingleOrDefaultAsync(x => x.Id == id);
        return item;
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<MovieTypeDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<MovieTypeDto>> ListAsync(
        [FromQuery] bool? hasTotal,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting,
        [FromServices] IEventBus eventBus)
    {
        var movies = _dbContext.MovieTypes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            movies = movies.Where(x => x.Name.Contains(q))
                         .Where(x => x.Remark.Contains(q));
            eventBus.RaiseEvent<UserSearchEvent>(new UserSearchEvent("MovieType", q));
        }

        var query = movies.Select(MovieTypeMapper.ProjectToDto);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.Id);
        }

        return await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);
    }
}
