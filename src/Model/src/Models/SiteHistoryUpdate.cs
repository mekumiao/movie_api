using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record SiteHistoryUpdate
{
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    [PathColumn, Url, Required]
    public string URL { get; set; } = string.Empty;
    public bool IsActivate { get; set; }
}
