using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.Events;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 主机配置
/// </summary>
public class HostConfigController : MyBaseController
{
    private readonly MovieDbContext _dbContext;

    public HostConfigController(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OutputResult<HostConfigDto>), StatusCodes.Status200OK)]
    public async Task<HostConfigDto?> GetAsync([FromRoute] long id)
    {
        var config = await _dbContext.HostConfigs.AsNoTracking()
                                                 .Select(HostConfigMapper.ProjectToDto)
                                                 .SingleOrDefaultAsync(x => x.Id == id);
        return config;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<HostConfigDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<HostConfigDto>> ListAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] bool? hasTotal,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting)
    {
        var hostConfigs = _dbContext.HostConfigs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            eventBus.RaiseEvent(new UserSearchEvent("HostConfig", q));

            hostConfigs = hostConfigs.Where(x => x.Name.Contains(q)
                                                 || x.Remark.Contains(q)
                                                 || x.Host.Contains(q));
        }

        var query = hostConfigs.Select(HostConfigMapper.ProjectToDto);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.Id);
        }

        var result = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<long>), StatusCodes.Status200OK)]
    public async Task<long> AddAsync(
        [FromServices] IHostConfigAddMapper mapper,
        [FromBody] HostConfigAdd input)
    {
        var add = mapper.Map(input);
        await _dbContext.AddAsync(add);
        await _dbContext.SaveChangesAsync();
        return add.Id;
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
            [FromServices] IHostConfigUpdateMapper mapper,
            [FromRoute, Range(1, long.MaxValue)] long id,
            [FromBody] HostConfigUpdate input)
    {
        var hostConfig = new HostConfig { Id = id };
        _dbContext.HostConfigs.Attach(hostConfig);
        mapper.Map(input, hostConfig);
        try
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync([FromRoute, Range(1, long.MaxValue)] long id)
    {
        var hostConfig = new HostConfig { Id = id };
        _dbContext.Attach(hostConfig);
        _dbContext.Remove(hostConfig);
        try
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException)
        {
            return default;
        }
    }
}
