using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public class ApkVersionUpdate
{
    [TextColumn, Required]
    public string Remark { get; set; } = string.Empty;
}
