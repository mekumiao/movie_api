using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.DAL;

namespace MovieAPI.Web.IntegrationTest;

[TestClass]
public class UserSearchHistoryTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/UserSearchHistory")]
    [DataTestMethod]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.AreEqual(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.MediaType);
        Assert.AreEqual(Encoding.UTF8.WebName, response.Content.Headers.ContentType!.CharSet);
    }

    [DataRow("Movie", "v1", 5)]
    [DataRow("Movie", "v4", 0)]
    [DataRow("MovieFile", "v4", 0)]
    [DataRow("MovieTest", "v1", 0)]
    [DataRow("MovieTest", "v16", 0)]
    [DataTestMethod]
    public async Task Delete_DeleteHandler_Returns(string tag, string value, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"api/UserSearchHistory/{tag}/?value={value}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(expected, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var any = await dbContext.UserSearchHistories.AnyAsync(x => x.Tag == tag && x.Value == value);
        Assert.IsFalse(any);
    }
}
