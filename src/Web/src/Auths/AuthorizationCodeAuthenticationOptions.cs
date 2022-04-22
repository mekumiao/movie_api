using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using MovieAPI.Common;

namespace MovieAPI.Web.Auths;

public class AuthorizationCodeAuthenticationOptions : AuthenticationSchemeOptions
{
    public string AuthorizationCode = "AuthorizationCode";

    public ClaimsPrincipal CreateClaimsPrincipal(string scheme, string authorizationCode)
    {
        var identity = new ClaimsIdentity(scheme, JwtClaimTypes.Name, JwtClaimTypes.Role);

        identity.AddClaim(new Claim(JwtClaimTypes.Role, MyConst.Role.Anonymous));
        identity.AddClaim(new Claim(JwtClaimTypes.Subject, MyConst.Role.AnonymousId.ToString()));
        identity.AddClaim(new Claim(JwtClaimTypes.Name, MyConst.User.Anonymous));
        identity.AddClaim(new Claim(JwtClaimTypes.NickName, MyConst.User.Anonymous));

        return new ClaimsPrincipal(identity);
    }

    public bool ValidateAuthorizationCode(string authorizationCode)
    {
        return authorizationCode == "123123";
    }
}
