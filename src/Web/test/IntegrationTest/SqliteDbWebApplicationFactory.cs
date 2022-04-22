using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieAPI.DAL;

namespace MovieAPI.Web.IntegrationTest;

public class SqliteDbWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
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

            services.AddDbContext<MovieDbContext>(options =>
            {
                options.UseSqlite("Data Source=SqliteDbForTesting.db;").UseExceptionProcessorSqlite();
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SqliteDbWebApplicationFactory<TStartup>>>();

            dbContext.Database.EnsureDeleted();
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
}
