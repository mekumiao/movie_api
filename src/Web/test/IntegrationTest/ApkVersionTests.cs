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
public class ApkVersionTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/apkVersion")]
    [DataRow("api/apkVersion/1")]
    [DataRow("api/apkVersion/latest")]
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
        var response = await client.DeleteAsync($"api/apkVersion/{id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(expected, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.ApkVersions.FindAsync(id);
        Assert.IsNull(item);
    }

    [DataRow(1)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var apkVersion = new ApkVersionUpdate
        {
            Remark = "xxx",
        };
        var response = await client.PutAsync($"api/apkVersion/{id}", JsonContent.Create(apkVersion));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Code);
        Assert.AreEqual(1, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.ApkVersions.FindAsync(id);
        Assert.IsNotNull(item);
        Assert.AreEqual(apkVersion.Remark, item.Remark);
    }

    [DataRow(100)]
    [DataTestMethod]
    public async Task Put_UpdateHandler_ReturnsNotExist(long id)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var siteHistory = new ApkVersionUpdate
        {
            Remark = "xxx",
        };
        var response = await client.PutAsJsonAsync($"api/apkVersion/{id}", siteHistory);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(ErrorCodes.NotExists, result.Code);
        Assert.AreEqual(0, result.Result);

        using var scope = _factory.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        var item = await dbContext.ApkVersions.FindAsync(id);
        Assert.IsNull(item);
    }

    [DataRow(1, 0, 1)]
    [DataRow(3, 0, 1)]
    [DataRow(4, 0, 1)]
    [DataRow(100, ErrorCodes.NotExists, 0)]
    [DataTestMethod]
    public async Task Put_ActivedHandler_Returns(long id, int code, int expected)
    {
        _factory.ReinitializeDb();
        var client = _factory.CreateClient();
        var response = await client.PutAsync($"api/ApkVersion/{id}/Actived", JsonContent.Create(true));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OutputResult<int>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(code, result.Code);
        Assert.AreEqual(expected, result.Result);
    }
}
