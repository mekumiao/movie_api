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
/// apk版本
/// </summary>
[Authorize(Roles = MyConst.Role.Admin)]
public class ApkVersionController : MyBaseController
{
    private readonly UploadFileRepository _uploadFileRepository;
    private readonly ILogger<ApkVersionController> _logger;
    private readonly MovieDbContext _dbContext;
    private readonly IOptionsMonitor<AppConfig> _optionsMonitor;

    public ApkVersionController(
        UploadFileRepository uploadFileRepository,
        MovieDbContext dbContext,
        IOptionsMonitor<AppConfig> optionsMonitor,
        ILogger<ApkVersionController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
        _optionsMonitor = optionsMonitor;
        _uploadFileRepository = uploadFileRepository;
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ApkVersionDto), StatusCodes.Status200OK)]
    public async Task<ApkVersionDto?> GetAsync([FromRoute] long id)
    {
        var item = await _dbContext.ApkVersions.AsNoTracking()
                                               .Select(ApkVersionMapper.ProjectToDto)
                                               .SingleOrDefaultAsync(x => x.Id == id);
        SchemeHostAdaptTo(item);
        return item;
    }

    /// <summary>
    /// 获取最新的版本发行版
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ApkVersionDto), StatusCodes.Status200OK)]
    public async Task<ApkVersionDto?> LatestAsync()
    {
        var query = _dbContext.ApkVersions.AsNoTracking()
                                          .Select(ApkVersionMapper.ProjectToDto)
                                          .OrderByDescending(x => x.VersionCode)
                                          .Where(x => x.IsActived);

        var item = await query.FirstOrDefaultAsync();
        SchemeHostAdaptTo(item);
        return item;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<ApkVersionDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<ApkVersionDto>> ListAsync(
        [FromServices] IEventBus eventBus,
        [FromQuery] bool? hasTotal,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting)
    {
        var query = _dbContext.ApkVersions.AsNoTracking().Select(ApkVersionMapper.ProjectToDto);

        if (!string.IsNullOrWhiteSpace(q))
        {
            eventBus.RaiseEvent(new UserSearchEvent("ApkVersion", q));
            query = query.Where(x => x.Name.Contains(q)
                                     || x.Remark.Contains(q)
                                     || x.VersionName.Contains(q));
        }

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            query = query.OrderByDescending(x => x.VersionCode)
                                     .ThenByDescending(x => x.IsActived);
        }

        var list = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);
        SchemeHostAdaptToRange(list);
        return list;
    }

    [HttpPut("[action]/{saveName}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<long>), StatusCodes.Status200OK)]
    public async Task<long> MergeAsync([FromRoute] string saveName)
    {
        var uploadFile = await _uploadFileRepository.GetAsync(saveName);
        if (uploadFile is null)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }

        var apkInfo = NETAaptHelper.GetApkInfo(uploadFile.FilePath);
        if (apkInfo is not null)
        {
            var apkFileName = $"{apkInfo.Name}-{apkInfo.VersionName}-{apkInfo.VersionCode}.apk";

            var item = await _dbContext.ApkVersions.SingleOrDefaultAsync(x => x.VersionCode == apkInfo.VersionCode
                                                                              && x.VersionName == apkInfo.VersionName);
            if (item is not null)
            {
                if (item.IsActived || item.Downloads > 0)
                {
                    HttpContext.AddErrorCode(102, "上传的apk的版本号或版本名在已激活或有下载量的列表中重复，请增加版本号和版本名后重试");
                    return default;
                }

                item.Name = apkInfo.Name;
                item.FileDiskURL = apkFileName;
            }
            else
            {
                item = new ApkVersion
                {
                    Name = apkInfo.Name,
                    VersionCode = apkInfo.VersionCode,
                    VersionName = apkInfo.VersionName,
                    FileDiskURL = apkFileName,
                };
                await _dbContext.ApkVersions.AddAsync(item);
            }

            _dbContext.UploadFiles.Remove(uploadFile);

            try
            {
                Directory.CreateDirectory(_optionsMonitor.CurrentValue.ApkDirectory);
                System.IO.File.Move(uploadFile.FilePath, Path.Combine(_optionsMonitor.CurrentValue.ApkDirectory, apkFileName), true);
                System.IO.File.Delete(uploadFile.FilePath);
                await _dbContext.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                HttpContext.AddErrorCode(102, "上传的apk的版本号或版本名在已激活或有下载量的列表中重复，请增加版本号和版本名后重试");
                return default;
            }
            return item.Id;
        }
        else
        {
            HttpContext.AddErrorCode(101, "解析apk文件出错，请联系管理员");
            return default;
        }
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync([FromRoute, Range(1, long.MaxValue)] long id)
    {
        var item = await _dbContext.ApkVersions.FindAsync(id);
        if (item is not null)
        {
            if (!item.IsActived && item.Downloads == 0)
            {
                _dbContext.ApkVersions.Remove(item);
                var result = await _dbContext.SaveChangesAsync();
                var apkPath = Path.Combine(_optionsMonitor.CurrentValue.ApkDirectory, item.FileDiskURL);
                try
                {
                    System.IO.File.Delete(apkPath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.LogWarning(ex, "删除apk文件{path}失败", apkPath);
                }
                return result;
            }
            else
            {
                HttpContext.AddErrorCode(101, "该版本已激活或已有下载量，不能删除");
                return default;
            }
        }
        return default;
    }

    [HttpPut("{id:long}/[action]")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<int> ActivedAsync(
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] bool isActived)
    {
        var item = new ApkVersion { Id = id, IsActived = !isActived };
        _dbContext.ApkVersions.Attach(item);
        item.IsActived = isActived;
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

    [HttpPut("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
        [FromServices] IApkVersionUpdateMapper mapper,
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] ApkVersionUpdate input)
    {
        var item = new ApkVersion { Id = id };
        _dbContext.ApkVersions.Attach(item);
        mapper.Map(input, item);
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
}
