using Microsoft.AspNetCore.Authentication;
using MovieAPI.Web.IntegrationTest.Auths;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthenticationBuilderExtension
{
    public static AuthenticationBuilder AddTest(this AuthenticationBuilder builder)
    {
        return builder.AddTest(TestAuthenticationDefaults.AuthenticationSchema);
    }

    public static AuthenticationBuilder AddTest(this AuthenticationBuilder builder, string schema)
    {
        return builder.AddTest(schema, _ => { });
    }

    public static AuthenticationBuilder AddTest(this AuthenticationBuilder builder, Action<TestAuthenticationOptions> configureOptions)
    {
        return builder.AddTest(TestAuthenticationDefaults.AuthenticationSchema, configureOptions);
    }

    public static AuthenticationBuilder AddTest(this AuthenticationBuilder builder, string schema, Action<TestAuthenticationOptions> configureOptions)
    {
        return builder.AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(schema, configureOptions);
    }
}
