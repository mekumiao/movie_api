namespace MovieAPI.DAL;

/// <summary>
/// 时间戳实体接口
/// </summary>
public interface ITimestampEntity
{
    /// <summary>
    /// 时间戳
    /// </summary>
    byte[] Timestamp { get; set; }
}
