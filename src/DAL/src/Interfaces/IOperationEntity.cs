namespace MovieAPI.DAL;

/// <summary>
/// 操作人表实体接口
/// </summary>
public interface IOperationEntity
{
    /// <summary>
    /// 创建人ID
    /// </summary>
    long? CreateUserId { get; set; }
    /// <summary>
    /// 修改人ID
    /// </summary>
    long? UpdateUserId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime? CreateTime { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? UpdateTime { get; set; }
}
