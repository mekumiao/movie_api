using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.Common.Test;

[TestClass]
public class SortingListTests
{
    [DataRow("name")]
    [DataRow("name asc")]
    [DataRow("name dsc")]
    [DataRow("name desc,age asc,sex")]
    [DataRow("name asc, age  asc, sex")]
    [DataTestMethod]
    public void TryParse_Returns_True(string sortingString)
    {
        Assert.IsTrue(SortingList.TryParse(sortingString, out _));
    }

    [TestMethod]
    public void TryParse_Returns_Sortings()
    {
        if (SortingList.TryParse("name desc,age asc,sex", out var sortings))
        {
            Assert.AreEqual(3, sortings.Count);

            Assert.AreEqual("name", sortings[0].Field);
            Assert.IsTrue(sortings[0].IsDescending);

            Assert.AreEqual("age", sortings[1].Field);
            Assert.IsFalse(sortings[1].IsDescending);

            Assert.AreEqual("sex", sortings[2].Field);
            Assert.IsFalse(sortings[2].IsDescending);
        }
        else
        {
            Assert.Fail();
        }
    }
}
