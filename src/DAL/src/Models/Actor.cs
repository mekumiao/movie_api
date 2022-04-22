using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.DAL;

/// <summary>
/// 演员
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Actor : TableEntity, IDeletedFlagEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 头像(一般存放第三方的网络图片地址)
    /// </summary>
    [PathColumn]
    public string Picture { get; set; } = string.Empty;
    /// <summary>
    /// 头像
    /// </summary>
    [PathColumn]
    public string PictureDiskURL { get; set; } = string.Empty;
    /// <summary>
    /// 描述
    /// </summary>
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    /// <summary>
    /// 电影集合
    /// </summary>
    [IgnoreDataMember]
    public IEnumerable<Movie>? Movies { get; set; }

    [IgnoreDataMember]
    public bool IsDeleted { get; set; }
}
