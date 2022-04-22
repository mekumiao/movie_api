using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MovieAPI.Domain.Core.Events;
using MovieAPI.Domain.Core.Notifications;

namespace MovieAPI.Domain.Core.Bus;

/// <summary>
/// 领域注册类
/// </summary>
public static class AddServices
{
    private static readonly ConcurrentDictionary<Type, IList<Type>> handlerMapping = new();
    /// <summary>
    /// 临时存储类型数组
    /// </summary>
    private static readonly Type[]? serviceTypes = Assembly.Load("MovieAPI.Domain").GetTypes();

    public static IList<Type> GetOrAddHandlerMapping(this Type eventType)
    {
        return handlerMapping.GetOrAdd(eventType, (Type type) => new List<Type>());
    }

    /// <summary>
    /// 注册事件总线
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="serviceDescriptors"></param>
    public static void AddEventBus<THandler, TEventArgs>(this IServiceCollection serviceDescriptors)
        where THandler : IEventHandler<TEventArgs>
        where TEventArgs : EventArgs
    {
        serviceDescriptors.TryAddSingleton<IEventBus, EventBus>();
        var serviceType = typeof(THandler);
        var implementationType = serviceTypes?.FirstOrDefault(s => serviceType.IsAssignableFrom(s));

        _ = implementationType ?? throw new ArgumentNullException(string.Format("类型{0}未找到实现类", serviceType.FullName));

        serviceDescriptors.AddTransient(serviceType, implementationType);

        GetOrAddHandlerMapping(typeof(TEventArgs)).Add(serviceType);
    }

    /// <summary>
    /// 注册验证错误通知
    /// </summary>、
    /// <param name="serviceDescriptors"></param>
    public static void AddNotifyValidation(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IEventHandler<NotifyValidation>, NotifyValidationHandler>();
        GetOrAddHandlerMapping(typeof(NotifyValidation)).Add(typeof(NotifyValidationHandler));
    }
}
