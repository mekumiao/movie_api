using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MovieAPI.DAL;

/// <summary>
/// 用户搜索历史
/// </summary>
public class UserSearchHistory : TableEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }
    /// <summary>
    /// 用户
    /// </summary>
    [ForeignKey(nameof(UserId))]
    [IgnoreDataMember]
    public User? User { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    [StringColumn]
    public string Value { get; set; } = string.Empty;
    /// <summary>
    /// 标签（一般用于标识用户搜索的类容）
    /// </summary>
    [StringColumn]
    public string Tag { get; set; } = string.Empty;
}
