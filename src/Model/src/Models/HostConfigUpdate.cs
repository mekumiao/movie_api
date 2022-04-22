using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record HostConfigUpdate
{
    [StringColumn, Required]
    public string Name { get; set; } = string.Empty;
    [StringColumn, Required, Url]
    public string Host { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
}
