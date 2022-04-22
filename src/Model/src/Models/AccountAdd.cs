using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public class AccountAdd
{
    [StringColumn, Required, MinLength(4)]
    public string Username { get; set; } = string.Empty;
    [StringColumn, Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
