using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Domain.Core;

/// <summary>
/// 资源类型
/// </summary>
public enum ResourceType : byte
{
    /// <summary>
    /// 其他
    /// </summary>
    [Display(Name = "其他")]
    Other = 0,
    /// <summary>
    /// 文档
    /// </summary>
    [Display(Name = "文档")]
    Document,
    /// <summary>
    /// 视频
    /// </summary>
    [Display(Name = "视频")]
    Video,
    /// <summary>
    /// 音频
    /// </summary>
    [Display(Name = "音频")]
    Audio,
    /// <summary>
    /// 图片
    /// </summary>
    [Display(Name = "图片")]
    Image
}
