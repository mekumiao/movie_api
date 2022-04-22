using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieAPI.DAL;
using MovieAPI.Web.IntegrationTest.Auths;

namespace MovieAPI.Web.IntegrationTest;

public class MemoryDbWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseUrls("http://localhost:9643");
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MovieDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IConfigureOptions<AuthorizationOptions>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddAuthentication().AddTest();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("api", options => options.RequireAuthenticatedUser()
                                                           .AddAuthenticationSchemes(TestAuthenticationDefaults.AuthenticationSchema));
                options.DefaultPolicy = options.GetPolicy("api")!;
            });

            services.AddDbContext<MovieDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting").UseExceptionProcessorSqlServer();
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<MemoryDbWebApplicationFactory<TStartup>>>();

            dbContext.Database.EnsureCreated();

            try
            {
                Utilities.InitializeDbForTests(dbContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
            }
        });
    }

    public void InitializeDb()
    {
        using var scope = this.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MemoryDbWebApplicationFactory<TStartup>>>();
        try
        {
            Utilities.InitializeDbForTests(dbContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
        }
    }

    public void ReinitializeDb()
    {
        using var scope = this.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MemoryDbWebApplicationFactory<TStartup>>>();
        try
        {
            Utilities.ReinitializeDbForTests(dbContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
        }
    }

    public JsonSerializerOptions GetSerializerOptions()
    {
        return this.Services.GetRequiredService<IOptions<JsonOptions>>().Value.SerializerOptions;
    }
}
