using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.DAL;
using MovieAPI.Model;
using MovieAPI.Services;

namespace MovieAPI.Web.IntegrationTest;

[TestClass]
public class MovieTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/Movie")]
    [DataRow("api/Movie/1")]
    [DataTestMethod]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.AreEqual(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.MediaType);
        Assert.AreEqual(Encoding.UTF8.WebName, response.Content.Headers.ContentType!.CharSet);
    }

    [DataRow(1, 1)]
    [DataRow(4, 0)]
    [DataTestMethod]
    public async Task Delete_DeleteMovieHandler_Returns(long id, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"api/Movie/{id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(expected, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Movies.FindAsync(id);
        Assert.IsNull(item);
    }

    [DataRow(1, 0, 1)]
    [DataRow(4, 1, 0)]
    [DataTestMethod]
    public async Task Put_DislikeHandler_Returns(long id, int code, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.PutAsync($"api/Movie/{id}/Dislike", JsonContent.Create(true));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(code, result.Code);
        Assert.AreEqual(expected, result.Result);
    }

    [DataRow(1, 0, 1)]
    [DataRow(4, 1, 0)]
    [DataTestMethod]
    public async Task Put_StarHandler_Returns(long id, int code, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.PutAsync($"api/Movie/{id}/Star", JsonContent.Create(true));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(code, result.Code);
        Assert.AreEqual(expected, result.Result);
    }


    [DataRow(1)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var movie = new MovieUpdate
        {
            ActorName = "ttt",
            Remark = "xxx",
        };

        var response = await client.PutAsJsonAsync($"api/Movie/{id}", movie);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.IsTrue(result.Result > 0);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Movies.Include(x => x.Actor)
                                         .SingleOrDefaultAsync(x => x.Id == id);
        Assert.IsNotNull(item);
        Assert.AreEqual(movie.ActorName, item.ActorName);
        Assert.AreEqual(movie.Remark, item.Remark);
        Assert.IsNotNull(item.Actor);
        Assert.AreEqual(movie.ActorName, item.Actor.Name);
    }

    [DataRow(100)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsNotExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var movie = new MovieUpdate
        {
            ActorName = "pppppp",
            Remark = "xxx",
        };

        var response = await client.PutAsJsonAsync($"api/Movie/{id}", movie);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(ErrorCodes.NotExists, result.Code);
        Assert.AreEqual(0, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Movies.FindAsync(id);
        Assert.IsNull(item);
    }

}
