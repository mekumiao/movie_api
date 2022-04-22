namespace MovieAPI.Domain.Core.Bus;

/// <summary>
/// 领域中间件（事件总线）
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// 触发领域事件
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="eventArgs"></param>
    void RaiseEvent<TEventArgs>(TEventArgs eventArgs) where TEventArgs : EventArgs;
    /// <summary>
    /// 触发领域事件
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="eventArgs"></param>
    Task RaiseEventAsync<TEventArgs>(TEventArgs eventArgs) where TEventArgs : EventArgs;
}
