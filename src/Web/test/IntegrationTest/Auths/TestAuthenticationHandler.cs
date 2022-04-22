using System.Security.Claims;
using System.Text.Encodings.Web;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieAPI.Common;

namespace MovieAPI.Web.IntegrationTest.Auths;

public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
{
    public TestAuthenticationHandler(
        IOptionsMonitor<TestAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    private ClaimsPrincipal CreateClaimsPrincipal()
    {
        var identity = new ClaimsIdentity(Scheme.Name, JwtClaimTypes.Name, JwtClaimTypes.Role);

        identity.AddClaim(new Claim(JwtClaimTypes.Subject, MyConst.User.AdminId.ToString()));
        identity.AddClaim(new Claim(JwtClaimTypes.Name, MyConst.User.Admin));
        identity.AddClaim(new Claim(JwtClaimTypes.Role, MyConst.Role.Admin));

        return new ClaimsPrincipal(identity);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var principal = CreateClaimsPrincipal();
        var ticket = new AuthenticationTicket(
            principal,
            Scheme.Name
        );
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
