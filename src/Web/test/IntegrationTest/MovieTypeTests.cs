using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.Web.IntegrationTest;

[TestClass]
public class MovieTypeTests
{
    [AllowNull]
    private static MemoryDbWebApplicationFactory<Program> _factory = new();

    [DataRow("api/MovieType")]
    [DataRow("api/MovieType/1")]
    [DataTestMethod]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.AreEqual(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.MediaType);
        Assert.AreEqual(Encoding.UTF8.WebName, response.Content.Headers.ContentType!.CharSet);
    }
}
