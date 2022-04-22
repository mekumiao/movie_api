using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record ActorUpdate
{
    [StringColumn, Required]
    public string Name { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    [StringColumn]
    public string? PictureSaveName { get; set; }
}
