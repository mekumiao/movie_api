using System.ComponentModel.DataAnnotations;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.Events;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;
using MovieAPI.Repository;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 演员
/// </summary>
public class ActorController : MyBaseController
{
    private readonly IOptionsMonitor<AppConfig> _optionsMonitor;
    private readonly UploadFileRepository _uploadFileRepository;
    private readonly MovieDbContext _dbContext;

    public ActorController(
        MovieDbContext dbContext,
        IOptionsMonitor<AppConfig> optionsMonitor,
        UploadFileRepository uploadFileRepository)
    {
        _dbContext = dbContext;
        _optionsMonitor = optionsMonitor;
        _uploadFileRepository = uploadFileRepository;
    }

    [HttpPost]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<long>), StatusCodes.Status200OK)]
    public async Task<long> AddAsync(
        [FromServices] IActorAddMapper mapper,
        [FromBody] ActorAdd input)
    {
        var uploadFile = await _uploadFileRepository.GetAsync(input.PictureSaveName);

        if (uploadFile is null)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }

        var saveDirectory = Path.Combine(_optionsMonitor.CurrentValue.PictureDirectory, "upload_actor");
        var diskurl = $"upload_actor/{uploadFile.SaveName}";

        var actor = await _dbContext.Actors.IgnoreQueryFilters()
                                           .SingleOrDefaultAsync(x => x.Name == input.Name);
        if (actor is not null)
        {
            if (actor.IsDeleted)
            {
                mapper.Map(input, actor);
                actor.IsDeleted = false;
                actor.PictureDiskURL = diskurl;
            }
            else
            {
                HttpContext.AddErrorCode(ErrorCodes.AlreadyExists, "ActorName的值已存在");
                return actor.Id;
            }
        }
        else
        {
            actor = mapper.Map(input);
            actor.PictureDiskURL = diskurl;
            await _dbContext.AddAsync(actor);
        }

        _dbContext.UploadFiles.Remove(uploadFile);

        try
        {
            Directory.CreateDirectory(saveDirectory);
            var savePath = Path.Combine(_optionsMonitor.CurrentValue.PictureDirectory, diskurl);
            System.IO.File.Move(uploadFile.FilePath, savePath);
            System.IO.File.Delete(uploadFile.FilePath);
            await _dbContext.SaveChangesAsync();
        }
        catch (UniqueConstraintException)
        {
            actor = await _dbContext.Actors.AsNoTracking()
                                           .IgnoreQueryFilters()
                                           .SingleOrDefaultAsync(x => x.Name == input.Name);
            if (actor is null)
            {
                HttpContext.AddErrorCode(ErrorCodes.SystemBusy);
                return default;
            }
        }

        return actor.Id;
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
        [FromServices] IActorUpdateMapper mapper,
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] ActorUpdate input)
    {
        var actor = await _dbContext.Actors.FindAsync(id);
        if (actor is not null)
        {
            mapper.Map(input, actor);

            if (!string.IsNullOrWhiteSpace(input.PictureSaveName))
            {
                var uploadFile = await _uploadFileRepository.GetAsync(input.PictureSaveName);
                if (uploadFile is not null)
                {
                    _dbContext.UploadFiles.Remove(uploadFile);
                    actor.PictureDiskURL = $"upload_actor/{uploadFile.SaveName}";
                    var saveDirectory = Path.Combine(_optionsMonitor.CurrentValue.PictureDirectory, "upload_actor");
                    var savePath = Path.Combine(_optionsMonitor.CurrentValue.PictureDirectory, actor.PictureDiskURL);
                    Directory.CreateDirectory(saveDirectory);
                    System.IO.File.Move(uploadFile.FilePath, savePath);
                    System.IO.File.Delete(uploadFile.FilePath);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }
        HttpContext.AddErrorCode(ErrorCodes.NotExists);
        return default;
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync([FromRoute, Range(1, long.MaxValue)] long id)
    {
        var actor = new Actor { Id = id };
        _dbContext.Actors.Attach(actor);
        actor.IsDeleted = true;
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return default;
        }
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OutputResult<ActorDto>), StatusCodes.Status200OK)]
    public async Task<ActorDto?> GetAsync([FromRoute] long id)
    {
        var actor = await _dbContext.Actors.AsNoTracking()
                                           .Select(ActorMapper.ProjectToDto)
                                           .SingleOrDefaultAsync(x => x.Id == id);
        SchemeHostAdaptTo(actor);
        return actor;
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<ActorDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<ActorDto>> ListAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] bool? hasTotal,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting)
    {
        var actors = _dbContext.Actors.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            actors = actors.Where(x => x.Remark.Contains(q)
                                       || x.Name.Contains(q));
            eventBus.RaiseEvent<UserSearchEvent>(new UserSearchEvent("Actor", q));
        }

        var query = actors.Select(ActorMapper.ProjectToDto);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.Id);
        }

        var items = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);

        SchemeHostAdaptToRange(items);

        return items;
    }
}
