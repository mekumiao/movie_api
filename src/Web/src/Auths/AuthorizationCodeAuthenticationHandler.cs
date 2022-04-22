using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace MovieAPI.Web.Auths;

public class AuthorizationCodeAuthenticationHandler : AuthenticationHandler<AuthorizationCodeAuthenticationOptions>
{
    public AuthorizationCodeAuthenticationHandler(
        IOptionsMonitor<AuthorizationCodeAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        await Task.CompletedTask;
        if (!Context.Request.Headers.TryGetValue(Options.AuthorizationCode, out var authorizationCode))
        {
            if (!Context.Request.Query.TryGetValue(Options.AuthorizationCode, out authorizationCode))
            {
                return AuthenticateResult.NoResult();
            }
        }

        if (!Options.ValidateAuthorizationCode(authorizationCode))
        {
            return AuthenticateResult.NoResult();
        }

        var principal = Options.CreateClaimsPrincipal(Scheme.Name, authorizationCode);
        var ticket = new AuthenticationTicket(
            principal,
            Scheme.Name
        );
        return AuthenticateResult.Success(ticket);
    }
}
