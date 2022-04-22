using Microsoft.AspNetCore.Authentication;

namespace MovieAPI.Web.Auths;

public class PasswordAuthenticationOptions : AuthenticationSchemeOptions
{
    public string Username = "Username";
    public string Password = "Password";
}
