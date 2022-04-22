using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.DAL.Test;

[TestClass]
public class MovieDbContextTests
{
    [TestMethod]
    public async Task Table_UserTest()
    {
        var sp = Utilities.InitializeDbForServiceProviderTests();
        using var scope = sp.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        Assert.IsTrue(await dbContext.Users.AnyAsync());
    }
}
