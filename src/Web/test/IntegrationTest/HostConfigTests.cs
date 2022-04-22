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
public class HostConfigTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/HostConfig")]
    [DataRow("api/HostConfig/1")]
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
        var response = await client.DeleteAsync($"api/HostConfig/{id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(expected, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.HostConfigs.FindAsync(id);
        Assert.IsNull(item);
    }

    [DataRow(1)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var hostConfig = new HostConfigUpdate
        {
            Name = "eeeeaaa",
            Host = "http://www.baidu.com",
            Remark = "xxxeeeee",
        };
        var response = await client.PutAsync($"api/HostConfig/{id}", JsonContent.Create(hostConfig));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(1, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.HostConfigs.FindAsync(id);
        Assert.IsNotNull(item);
        Assert.AreEqual(hostConfig.Name, item.Name);
        Assert.AreEqual(hostConfig.Remark, item.Remark);
        Assert.AreEqual(hostConfig.Host, item.Host);
    }

    [DataRow(100)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsNotExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var hostConfig = new HostConfigUpdate
        {
            Name = "eeeeaaa",
            Host = "http://www.baidu.com",
            Remark = "xxxeeeee",
        };
        var response = await client.PutAsJsonAsync($"api/HostConfig/{id}", hostConfig);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(ErrorCodes.NotExists, result.Code);
        Assert.AreEqual(0, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.HostConfigs.FindAsync(id);
        Assert.IsNull(item);
    }

    [TestMethod]
    public async Task Post_AddHandler_Returns()
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var siteHistory = new HostConfigAdd
        {
            Name = "eeeeaaa",
            Host = "http://www.baidu.com",
            Remark = "xxxeeeee",
        };
        var response = await client.PostAsJsonAsync($"api/HostConfig", siteHistory);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<long>>(_factory.GetSerializerOptions());
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.IsTrue(result.Result > 0);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.HostConfigs.FindAsync(result.Result);
        Assert.IsNotNull(item);
        Assert.AreEqual(siteHistory.Name, item.Name);
        Assert.AreEqual(siteHistory.Remark, item.Remark);
        Assert.AreEqual(siteHistory.Host, item.Host);
    }
}
