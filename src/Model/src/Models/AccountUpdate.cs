using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record AccountUpdate
{
    [StringColumn, MinLength(6), Required]
    public string OldPassword { get; set; } = string.Empty;
    [StringColumn, MinLength(6), Required]
    public string NewPassword { get; set; } = string.Empty;
}
