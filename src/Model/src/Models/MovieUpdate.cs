using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record MovieUpdate
{
    /// <summary>
    /// 演员名称
    /// </summary>
    [StringColumn, Required]
    public string ActorName { get; set; } = string.Empty;
    /// <summary>
    /// 描述
    /// </summary>
    [TextColumn]
    public string Remark { get; set; } = string.Empty;
    /// <summary>
    /// 图片上传接口返回的图片名称
    /// </summary>
    public string? PictureSaveName { get; set; }
}
