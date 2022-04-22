using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieAPI.DAL;
using MovieAPI.Model;
using MovieAPI.Services;

namespace MovieAPI.Web.IntegrationTest;

[TestClass]
public class ActorTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/Actor")]
    [DataRow("api/Actor/1")]
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
    [DataRow(100, 0)]
    [DataTestMethod]
    public async Task Delete_DeleteHandler_Returns(long id, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"api/Actor/{id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(expected, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Actors.FindAsync(id);
        Assert.IsNull(item);
    }

    [DataRow(1)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var actor = new ActorUpdate
        {
            Name = "ttt",
            Remark = "xxx",
        };

        var response = await client.PutAsJsonAsync($"api/Actor/{id}", actor);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(1, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Actors.FindAsync(id);
        Assert.IsNotNull(item);
        Assert.AreEqual(actor.Name, item.Name);
        Assert.AreEqual(actor.Remark, item.Remark);
    }

    [DataRow(100)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsNotExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var actor = new ActorUpdate
        {
            Name = "pppppp",
            Remark = "xxx",
        };

        var response = await client.PutAsJsonAsync($"api/Actor/{id}", actor);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(ErrorCodes.NotExists, result.Code);
        Assert.AreEqual(0, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Actors.FindAsync(id);
        Assert.IsNull(item);
    }

    // [TestMethod]
    public async Task Post_AddHandler_Returns()
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var actor = new ActorAdd
        {
            Name = "yyyyyy",
            Remark = "xxx",
        };

        var response = await client.PostAsJsonAsync($"api/Actor", actor);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<long>>(_factory.GetSerializerOptions());
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.IsTrue(result.Result > 0);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Actors.FindAsync(result.Result);
        Assert.IsNotNull(item);
        Assert.AreEqual(actor.Name, item.Name);
        Assert.AreEqual(actor.Remark, item.Remark);
    }

    // [TestMethod]
    public async Task Post_AddHandler_Returns_NameUniqueAndReAdd()
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var actor = new ActorAdd
        {
            Name = "yyyyyy",
            Remark = "xxx",
        };

        // 第一次添加 应该成功
        var response = await client.PostAsJsonAsync($"api/Actor", actor);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<long>>(_factory.GetSerializerOptions());
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.IsTrue(result.Result > 0);

        var id = result.Result;
        //第二次添加 应该失败
        response = await client.PostAsJsonAsync($"api/Actor", actor);
        response.EnsureSuccessStatusCode();

        result = await response.Content.ReadFromJsonAsync<OutputResult<long>>(_factory.GetSerializerOptions());
        Assert.IsNotNull(result);
        Assert.AreEqual(ErrorCodes.AlreadyExists, result.Code);
        Assert.AreEqual(id, result.Result);

        // 删除
        response = await client.DeleteAsync($"api/Actor/{id}");
        response.EnsureSuccessStatusCode();

        // 第三次添加 应该成功
        response = await client.PostAsJsonAsync($"api/Actor", actor);
        response.EnsureSuccessStatusCode();

        result = await response.Content.ReadFromJsonAsync<OutputResult<long>>(_factory.GetSerializerOptions());
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.IsTrue(result.Result > 0);

        // 校验数据库数据
        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.Actors.FindAsync(result.Result);
        Assert.IsNotNull(item);
        Assert.AreEqual(actor.Name, item.Name);
        Assert.AreEqual(actor.Remark, item.Remark);
        Assert.AreEqual(id, item.Id);
    }
}
