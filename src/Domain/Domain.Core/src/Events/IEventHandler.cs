namespace MovieAPI.Domain.Core.Events;

/// <summary>
/// 标记接口
/// </summary>
public interface IEventHandler
{

}

/// <summary>
/// 领域命令处理接口
/// </summary>
/// <typeparam name="TEventArgs"></typeparam>
public interface IEventHandler<TEventArgs> : IEventHandler where TEventArgs : EventArgs
{
    /// <summary>
    /// 处理发布的事件
    /// </summary>
    /// <param name="eventArgs"></param>
    void Handler(TEventArgs eventArgs);
}
