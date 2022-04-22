using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieAPI.Services;

namespace MovieAPI.Web.UnitTest.Extensions;

[TestClass]
public class HttpContextExtensionTests
{
    [TestMethod]
    public void ErrorMessages_Any()
    {
        Assert.IsTrue(HttpContextExtensions.ErrorMessages.Any());
    }

    [TestMethod]
    public void ErrorMessages_Length()
    {
        var count = typeof(ErrorCodes).GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        Assert.AreEqual(count, HttpContextExtensions.ErrorMessages.Keys.Count());
    }

    [TestMethod]
    public void ErrorMessages_AllValue_NotEmpty()
    {
        foreach (var item in HttpContextExtensions.ErrorMessages.Values)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(item));
        }
    }

    [DataRow(ErrorCodes.NotExists)]
    [DataRow(ErrorCodes.SystemBusy)]
    [DataRow(ErrorCodes.AlreadyExists)]
    [DataTestMethod]
    public void AddErrorCode(int code)
    {
        var dict = new Dictionary<object, object?>();
        var mock = new Mock<HttpContext>();
        mock.Setup(x => x.Items).Returns(dict);
        var httpContext = mock.Object;

        HttpContextExtensions.AddErrorCode(httpContext, code);

        Assert.IsTrue(dict.ContainsKey("errorcode"));
        Assert.IsInstanceOfType(dict["errorcode"], typeof(List<ErrorCode>));

        var list = dict["errorcode"] as List<ErrorCode>;

        Assert.IsNotNull(list);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(code, list[0].Code);
        Assert.IsFalse(string.IsNullOrWhiteSpace(list[0].Error));
    }

    [DataRow(ErrorCodes.NotExists, "不存在")]
    [DataRow(100001, "100001")]
    [DataRow(100002, "100002")]
    [DataRow(100003, "")]
    [DataRow(100004, null)]
    [DataRow(100004, "100004")]
    [DataTestMethod]
    public void AddErrorCode_InputWithError(int code, string? error)
    {
        var dict = new Dictionary<object, object?>();
        var mock = new Mock<HttpContext>();
        mock.Setup(x => x.Items).Returns(dict);
        var httpContext = mock.Object;

        HttpContextExtensions.AddErrorCode(httpContext, code, error);

        Assert.IsTrue(dict.ContainsKey("errorcode"));
        Assert.IsInstanceOfType(dict["errorcode"], typeof(List<ErrorCode>));

        var list = dict["errorcode"] as List<ErrorCode>;

        Assert.IsNotNull(list);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(code, list[0].Code);
        Assert.AreEqual(error, list[0].Error);
    }

    [TestMethod]
    public void ClearErrorCode()
    {
        var dict = new Dictionary<object, object?>();
        var mock = new Mock<HttpContext>();
        mock.Setup(x => x.Items).Returns(dict);
        var httpContext = mock.Object;

        HttpContextExtensions.AddErrorCode(httpContext, 101);
        HttpContextExtensions.AddErrorCode(httpContext, 102);
        HttpContextExtensions.AddErrorCode(httpContext, 103);

        HttpContextExtensions.ClearErrorCode(httpContext);

        Assert.IsTrue(dict.Any());
        Assert.IsTrue(dict.ContainsKey("errorcode"));
        Assert.IsInstanceOfType(dict["errorcode"], typeof(List<ErrorCode>));

        var list = dict["errorcode"] as List<ErrorCode>;

        Assert.IsNotNull(list);
        Assert.IsFalse(list.Any());
    }

    [TestMethod]
    public void GetLastErrorCode()
    {
        var dict = new Dictionary<object, object?>();
        var mock = new Mock<HttpContext>();
        mock.Setup(x => x.Items).Returns(dict);
        var httpContext = mock.Object;

        HttpContextExtensions.AddErrorCode(httpContext, 101);
        HttpContextExtensions.AddErrorCode(httpContext, 102);
        HttpContextExtensions.AddErrorCode(httpContext, 103);

        var last = HttpContextExtensions.GetLastErrorCode(httpContext);
        var list = dict["errorcode"] as List<ErrorCode>;

        Assert.IsNotNull(last);
        Assert.IsNotNull(list);
        Assert.AreEqual(list[^1], last);
    }
}
