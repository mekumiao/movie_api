using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.Web.UnitTest.Controllers;

[TestClass]
public class MyBaseControllerTests
{
    [TestMethod]
    public void SchemeHostAdaptTo()
    {
        var aa = new[] { "" };

        if (aa is not IEnumerable)
        {
            Assert.Fail();
        }
    }
}
