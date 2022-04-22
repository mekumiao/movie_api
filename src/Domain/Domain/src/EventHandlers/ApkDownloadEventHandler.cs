using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Events;
using MovieAPI.Domain.Events;

namespace MovieAPI.Domain.EventHandlers;

public class ApkDownloadEventHandler : IEventHandler<ApkDownloadEvent>
{
    private readonly ILogger<ApkDownloadEventHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public ApkDownloadEventHandler(
        ILogger<ApkDownloadEventHandler> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public void Handler(ApkDownloadEvent command)
    {
        _logger.LogDebug("处理apk下载事件");
        using var scope = _scopeFactory.CreateAsyncScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

        var tableName = nameof(dbContext.ApkVersions);
        var downloadsName = nameof(ApkVersion.Downloads);
        var fileDiskUrlName = nameof(ApkVersion.FileDiskURL);

        var sql = $"update {tableName} set {downloadsName}={downloadsName}+1 where {fileDiskUrlName}={{0}}";

        dbContext.Database.ExecuteSqlRaw(sql, command.FileName);

        _logger.LogDebug("处理apk下载事件结束，写入数据库并保存成功");
    }
}
