namespace MovieAPI.DAL;

/// <summary>
/// 删除标志接口
/// </summary>
public interface IDeletedFlagEntity
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    bool IsDeleted { get; set; }
}
