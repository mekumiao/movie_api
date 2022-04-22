using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MovieAPI.DAL;

/// <summary>
/// 电影
/// </summary>
public class Movie : TableEntity, IDeletedFlagEntity
{
    /// <summary>
    /// 详细页的相对路径
    /// </summary>
    [PathColumn]
    [IgnoreDataMember]
    public string DetailRelativeURL { get; set; } = string.Empty;
    /// <summary>
    /// 名称
    /// </summary>
    [RemarkColumn]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 图片路径
    /// </summary>
    [PathColumn]
    [IgnoreDataMember]
    public string Picture { get; set; } = string.Empty;
    /// <summary>
    /// 图片的磁盘路径
    /// </summary>
    [PathColumn]
    public string PictureDiskURL { get; set; } = string.Empty;
    /// <summary>
    /// 是否已经下载图片到本地
    /// </summary>
    public bool HasPicture { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [TextColumn]
    public string Remark { get; set; } = string.Empty;
    /// <summary>
    /// 演员ID
    /// </summary>
    public long? ActorId { get; set; }
    /// <summary>
    /// 演员
    /// </summary>
    [ForeignKey(nameof(ActorId))]
    [IgnoreDataMember]
    public Actor? Actor { get; set; }
    /// <summary>
    /// 演员名称
    /// </summary>
    [StringColumn]
    public string ActorName { get; set; } = string.Empty;
    /// <summary>
    /// 发行时间
    /// </summary>
    public DateTime? PushTime { get; set; }
    /// <summary>
    /// 电影文件ID
    /// </summary>
    public long? MovieFileId { get; set; }
    /// <summary>
    /// 电影文件
    /// </summary>
    [ForeignKey(nameof(MovieFileId))]
    public MovieFile? MovieFile { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    [PathColumn]
    public string MovieFileName { get; set; } = string.Empty;
    /// <summary>
    /// 是否有电影文件
    /// </summary>
    public bool HasMovieFile { get; set; }
    /// <summary>
    /// 资源链接
    /// </summary>
    [PathColumn]
    public string ResourceLink { get; set; } = string.Empty;
    /// <summary>
    /// 电影分类ID
    /// </summary>
    public long? MovieTypeId { get; set; }
    /// <summary>
    /// 电影分类
    /// </summary>
    [IgnoreDataMember]
    [ForeignKey(nameof(MovieTypeId))]
    public MovieType? MovieType { get; set; }
    /// <summary>
    /// 电影分类名称
    /// </summary>
    [StringColumn]
    public string MovieTypeName { get; set; } = string.Empty;

    [IgnoreDataMember]
    public ICollection<UserMovie>? UserMovies { get; set; }

    [IgnoreDataMember]
    public bool IsDeleted { get; set; }
}
