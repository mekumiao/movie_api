using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Events;
using MovieAPI.Domain.Events;

namespace MovieAPI.Domain.EventHandlers;

public class UserSearchEventHandler : IEventHandler<UserSearchEvent>
{
    private readonly ILogger<UserSearchEventHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IUser _user;

    public UserSearchEventHandler(
        IUser user,
        ILogger<UserSearchEventHandler> logger,
        IServiceScopeFactory scopeFactory)
    {
        _user = user;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public void Handler(UserSearchEvent command)
    {
        _logger.LogDebug("处理用户搜索事件");
        using var scope = _scopeFactory.CreateAsyncScope();
        using var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var history = new UserSearchHistory
        {
            UserId = _user.Id,
            Tag = command.Tag,
            Value = command.Value
        };
        context.UserSearchHistories.Add(history);
        context.SaveChanges();
        _logger.LogDebug("处理用户搜索事件结束，写入数据库并保存成功");
    }
}
