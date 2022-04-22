using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.Events;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 文件
/// </summary>
public class FileController : MyBaseController
{
    private readonly ILogger<FileController> _logger;
    private readonly MovieDbContext _dbContext;
    private readonly IUser _user;

    public FileController(
        IUser user,
        ILogger<FileController> logger,
        MovieDbContext dbContext)
    {
        _user = user;
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{saveName}")]
    [ProducesResponseType(typeof(OutputResult<UploadFileDto>), StatusCodes.Status200OK)]
    public async Task<UploadFileDto?> GetAsync(
        [FromServices] IUploadFileMapper mapper,
        [FromRoute] string saveName)
    {
        var query = _dbContext.UploadFiles.AsNoTracking()
                                          .Select(mapper.Projection);

        if (!_user.IsAdmin)
        {
            query = query.Where(x => x.CreateUserId == _user.Id);
        }

        var item = await query.SingleOrDefaultAsync(x => x.SaveName == saveName);
        SchemeHostAdaptTo(item);
        return item;
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="mapper"></param>
    /// <param name="q"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <param name="hasTotal"></param>
    /// <param name="all">获取全部用户的文件(仅管理员生效)。default is false</param>
    /// <param name="sorting"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<UploadFileDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<UploadFileDto>> ListAsync(
        [FromServices] IEventBus eventBus,
        [FromServices] IUploadFileMapper mapper,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery] bool? hasTotal,
        [FromQuery] bool? all,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting)
    {
        var uploadFiles = _dbContext.UploadFiles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            eventBus.RaiseEvent(new UserSearchEvent("File", q));
            uploadFiles = uploadFiles.Where(x => x.UploadName.Contains(q)
                                                 || x.Remark.Contains(q));
        }

        if (!(all is true && _user.IsAdmin))
        {
            uploadFiles = uploadFiles.Where(x => x.CreateUserId == _user.Id);
        }

        var query = uploadFiles.Select(mapper.Projection);

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.CreateTime)
                         .ThenByDescending(x => x.UpdateTime);
        }

        var list = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);
        SchemeHostAdaptToRange(list);
        return list;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OutputResult<string>), StatusCodes.Status200OK)]
    public async Task<string> AddAsync(
        [FromServices] IOptionsMonitor<AppConfig> optionsMonitor,
        [FromForm, RemarkColumn] string? remark,
        [Required] IFormFile file)
    {
        var item = new UploadFile();

        if (!string.IsNullOrWhiteSpace(remark))
        {
            item.Remark = remark;
        }

        item.Ext = Path.GetExtension(file.FileName);
        item.UploadName = StringColumnAttribute.EnsureOkLength(file.FileName);
        item.SaveName = $"{Guid.NewGuid():N}{item.Ext}";
        item.FilePath = Path.Combine(optionsMonitor.CurrentValue.UploadDirectory, item.SaveName);

        try
        {
            Directory.CreateDirectory(optionsMonitor.CurrentValue.UploadDirectory);
            using var fileStream = System.IO.File.Create(item.FilePath);
            await file.CopyToAsync(fileStream);

            await _dbContext.UploadFiles.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item.SaveName;
        }
        catch (UnauthorizedAccessException)
        {
            HttpContext.AddErrorCode(101, "系统没有磁盘的写权限，请联系管理员");
            return string.Empty;
        }
        catch (DirectoryNotFoundException)
        {
            HttpContext.AddErrorCode(102, "UploadDirectory文件夹配置有误，请联系管理员");
            return string.Empty;
        }
    }

    [HttpPut("{saveName}")]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
        [FromServices] IUploadFileUpdateMapper mapper,
        [FromRoute] string saveName,
        [FromBody] UploadFileUpdate input)
    {
        var query = _dbContext.UploadFiles.AsNoTracking()
                                          .Where(x => x.SaveName == saveName);

        if (!_user.IsAdmin)
        {
            query = query.Where(x => x.CreateUserId == _user.Id);
        }

        var exists = await query.AnyAsync();

        if (exists is false)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }

        var item = new UploadFile { SaveName = saveName };
        _dbContext.UploadFiles.Attach(item);
        mapper.Map(input);
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

    [HttpDelete("{saveName}")]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync([FromRoute] string saveName)
    {
        var item = await _dbContext.UploadFiles.FindAsync(saveName);
        if (item is null)
        {
            return default;
        }

        if (!_user.IsAdmin && item.CreateUserId != _user.Id)
        {
            return default;
        }

        _dbContext.UploadFiles.Remove(item);

        try
        {
            var result = await _dbContext.SaveChangesAsync();
            System.IO.File.Delete(item.FilePath);
            return result;
        }
        catch (DbUpdateConcurrencyException)
        {
            return default;
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "由于未找到路径{dir}，删除文件失败", item.FilePath);
            return default;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "系统无权限访问路径{dir}，请联系管理员", item.FilePath);
            return default;
        }
    }
}
