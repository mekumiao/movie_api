using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public class LoginAdd
{
    [DefaultValue("admin")]
    [StringColumn, Required]
    public string Username { get; set; } = string.Empty;
    [DefaultValue("123123")]
    [StringColumn, Required]
    public string Password { get; set; } = string.Empty;
}
