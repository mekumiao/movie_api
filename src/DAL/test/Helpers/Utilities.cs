using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MovieAPI.Common;

namespace MovieAPI.DAL.Test;

public static class Utilities
{
    public static IServiceProvider InitializeDbForServiceProviderTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton(CreateUser());
        services.AddSingleton(new Snowflake(5, 5));
        services.AddDbContext<MovieDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDbForTesting").UseExceptionProcessorSqlServer();
        });
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        dbContext.Database.EnsureCreated();
        return sp;
    }

    private static IUser CreateUser()
    {
        var mock = new Mock<IUser>();
        mock.Setup(x => x.Id).Returns(MyConst.User.AdminId);
        return mock.Object;
    }
}
