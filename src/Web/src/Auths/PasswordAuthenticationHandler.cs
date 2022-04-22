using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MovieAPI.Model;
using MovieAPI.Repository;
using MovieAPI.Web.Controllers;

namespace MovieAPI.Web.Auths;

public class PasswordAuthenticationHandler : AuthenticationHandler<PasswordAuthenticationOptions>
{
    public PasswordAuthenticationHandler(
        IOptionsMonitor<PasswordAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        await Task.CompletedTask;
        if (!Context.Request.Headers.TryGetValue(Options.Username, out var username))
        {
            if (!Context.Request.Query.TryGetValue(Options.Username, out username))
            {
                return AuthenticateResult.NoResult();
            }
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            return AuthenticateResult.NoResult();
        }

        if (!Context.Request.Headers.TryGetValue(Options.Password, out var password))
        {
            if (!Context.Request.Query.TryGetValue(Options.Password, out password))
            {
                return AuthenticateResult.NoResult();
            }
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return AuthenticateResult.NoResult();
        }

        using var scope = Context.RequestServices.CreateScope();
        using var repository = scope.ServiceProvider.GetRequiredService<AccountRepository>();

        var userId = (long?)default;

        try
        {
            userId = await repository.ValidateLoginAsync(new LoginAdd
            {
                Username = username,
                Password = password,
            });

            if (userId is null)
            {
                return AuthenticateResult.NoResult();
            }
        }
        catch (UsernameNotExistsException)
        {
            return AuthenticateResult.NoResult();
        }
        catch (PasswordErrorException)
        {
            return AuthenticateResult.NoResult();
        }

        var userDto = await repository.GetUserDtoAsync(userId.Value);

        if (userDto is null)
        {
            return AuthenticateResult.NoResult();
        }

        var principal = AccountController.CreateClaimsPrincipal(Scheme.Name, userDto);
        var ticket = new AuthenticationTicket(
            principal,
            Scheme.Name
        );
        return AuthenticateResult.Success(ticket);
    }
}
