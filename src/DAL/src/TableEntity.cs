using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MovieAPI.DAL;

/// <summary>
/// 有主键表实体
/// </summary>
public abstract class TableEntity : IKeyEntity, IOperationEntity, ITable
{
    /// <summary>
    /// 主键
    /// </summary>
    [Key]
    public long Id { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [IgnoreDataMember]
    public long? CreateUserId { get; set; }
    /// <summary>
    /// 修改人ID
    /// </summary>
    [IgnoreDataMember]
    public long? UpdateUserId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [IgnoreDataMember]
    public DateTime? CreateTime { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    [IgnoreDataMember]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    [IgnoreDataMember]
    public User? CreateUser { get; set; }
    /// <summary>
    /// 修改人
    /// </summary>
    [IgnoreDataMember]
    public User? UpdateUser { get; set; }
}
