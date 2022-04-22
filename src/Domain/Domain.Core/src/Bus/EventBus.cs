using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieAPI.Domain.Core.Events;

namespace MovieAPI.Domain.Core.Bus;

/// <summary>
/// 领域中间件
/// </summary>
public sealed class EventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventBus> _logger;

    public EventBus(ILogger<EventBus> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void RaiseEvent<TEventArgs>(TEventArgs eventArgs) where TEventArgs : EventArgs
    {
        try
        {
            IList<Type> types = typeof(TEventArgs).GetOrAddHandlerMapping();
            if (types == null || types.Count == 0)
            {
                throw new ServiceException("事件总线未注册：" + typeof(TEventArgs).Name);
            }
            foreach (var type in types)
            {
                object obj = _serviceProvider.GetRequiredService(type);
                if (type.IsAssignableFrom(obj.GetType()))
                {
                    if (obj is IEventHandler<TEventArgs> handler)
                    {
                        handler.Handler(eventArgs);
                    }
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "MovieAPI.Domain.Core.Bus.EventBus", e.Message);
        }
    }

    public async Task RaiseEventAsync<TEventArgs>(TEventArgs eventData) where TEventArgs : EventArgs
    {
        try
        {
            IList<Type> types = typeof(TEventArgs).GetOrAddHandlerMapping();
            if (types == null || types.Count == 0)
            {
                throw new ServiceException("事件总线未注册：" + typeof(TEventArgs).Name);
            }
            foreach (var type in types)
            {
                var obj = _serviceProvider.GetRequiredService(type);
                if (type.IsAssignableFrom(obj.GetType()))
                {
                    if (obj is IEventHandler<TEventArgs> handler)
                    {
                        await Task.Run(() => { handler.Handler(eventData); });
                    }
                }
            }
        }
        catch (AggregateException e)
        {
            _logger.LogError(e, "MovieAPI.Domain.Core.Bus.EventBus", e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "MovieAPI.Domain.Core.Bus.EventBus", e.Message);
        }
    }
}
