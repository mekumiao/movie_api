using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record ActorAdd
{
    [StringColumn, Required]
    public string Name { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    /// <summary>
    /// 图片的保存名称
    /// </summary>
    [Required, StringColumn]
    public string PictureSaveName { get; set; } = string.Empty;
}
