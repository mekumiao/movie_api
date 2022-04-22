using MovieAPI.DAL;

namespace MovieAPI.Model;

public record MovieAdd
{
    /// <summary>
    /// Relative URL
    /// </summary>
    [PathColumn]
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
    public string Picture { get; set; } = string.Empty;
    /// <summary>
    /// 图片在磁盘上的URL
    /// </summary>
    [StringColumn]
    public string PictureDiskURL { get; set; } = string.Empty;
    /// <summary>
    /// 描述
    /// </summary>
    [TextColumn]
    public string Remark { get; set; } = string.Empty;
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
    /// 电影文件名称
    /// </summary>
    [PathColumn]
    public string MovieFileName { get; set; } = string.Empty;
    /// <summary>
    /// 资源链接
    /// </summary>
    [PathColumn]
    public string ResourceLink { get; set; } = string.Empty;
    /// <summary>
    /// 电影分类名称
    /// </summary>
    [StringColumn]
    public string MovieTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 裁剪字符串
    /// </summary>
    public void Cutting()
    {
        DetailRelativeURL = DetailRelativeURL.Trim();
        Name = Name.Trim();
        Picture = Picture.Trim();
        PictureDiskURL = PictureDiskURL.Trim();
        Remark = Remark.Trim();
        ActorName = ActorName.Trim();
        MovieFileName = MovieFileName.Trim();
        ResourceLink = ResourceLink.Trim();
        MovieTypeName = MovieTypeName.Trim();

        if (DetailRelativeURL is { Length: > 500 })
        {
            DetailRelativeURL = DetailRelativeURL[..500];
        }

        if (Name is { Length: > 255 })
        {
            Name = Name[..255];
        }

        if (Picture is { Length: > 500 })
        {
            Picture = Picture[..500];
        }

        if (PictureDiskURL is { Length: > 40 })
        {
            PictureDiskURL = PictureDiskURL[..40];
        }

        if (Remark is { Length: > 1000 })
        {
            Remark = Remark[..1000];
        }

        if (ActorName is { Length: > 40 })
        {
            ActorName = ActorName[..40];
        }

        if (MovieFileName is { Length: > 500 })
        {
            MovieFileName = MovieFileName[..500];
        }

        if (ResourceLink is { Length: > 500 })
        {
            ResourceLink = ResourceLink[..500];
        }

        if (MovieTypeName is { Length: > 40 })
        {
            MovieTypeName = MovieTypeName[..40];
        }
    }
}
