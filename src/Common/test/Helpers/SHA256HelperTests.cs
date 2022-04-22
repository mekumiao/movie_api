using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.Common.Test;

[TestClass]
public class SHA256HelperTests
{
    [TestMethod]
    public void HashString()
    {
        var text = SHA256Helper.HashToHexString("wangsir123123");
        Assert.IsNotNull(text);
        Assert.AreEqual(MyConst.User.DefaultPassword, text);
    }
}
