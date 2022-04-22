using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieAPI.DAL.Test;

[TestClass]
public class PaginatedListTest
{
    private static PaginatedList<int> CreatePaginatedList(int total, int size)
    {
        var query = Enumerable.Range(0, total).AsQueryable();
        return new PaginatedList<int>(query, 1, size);
    }

    [TestMethod]
    public void DefaultValue()
    {
        var paginated = CreatePaginatedList(100, 10);
        Assert.AreEqual(0, paginated.Total);
        Assert.AreEqual(10, paginated.Size);
        Assert.AreEqual(1, paginated.No);
        Assert.IsFalse(paginated.HasTotal);
        Assert.AreEqual(0, paginated.MaxNo);
        Assert.AreEqual(0, paginated.Count);
    }

    [DataRow(1)]
    [DataRow(2)]
    [DataRow(10)]
    [DataTestMethod]
    public void JumpTo_EffectiveNo(int no)
    {
        var paginated = CreatePaginatedList(100, 10);

        Assert.IsTrue(paginated.JumpTo());
        Assert.AreEqual(1, paginated.No);
        Assert.AreEqual(10, paginated.Count);
        Assert.IsTrue(paginated.JumpTo(no));
        Assert.AreEqual(no, paginated.No);
        Assert.AreEqual(10, paginated.Count);
    }

    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(-100)]
    [DataTestMethod]
    public void JumpTo_InvalidNo(int no)
    {
        var paginated = CreatePaginatedList(100, 10);

        Assert.IsTrue(paginated.JumpTo());
        Assert.AreEqual(1, paginated.No);
        Assert.AreEqual(10, paginated.Count);
        Assert.IsTrue(paginated.JumpTo(no));
        Assert.AreEqual(1, paginated.No);
        Assert.AreEqual(10, paginated.Count);
    }

    [DataRow(50)]
    [DataRow(100)]
    [DataRow(1000)]
    [DataTestMethod]
    public void JumpTo_ExceedNo(int no)
    {
        var paginated = CreatePaginatedList(100, 10);

        Assert.IsTrue(paginated.JumpTo());
        Assert.AreEqual(1, paginated.No);
        Assert.AreEqual(10, paginated.Count);
        Assert.IsFalse(paginated.JumpTo(no));
        Assert.AreEqual(no, paginated.No);
        Assert.AreEqual(0, paginated.Count);
    }

    [TestMethod]
    public void JumpToNext()
    {
        var paginated = CreatePaginatedList(100, 10);

        Assert.IsTrue(paginated.JumpToNext());
        Assert.AreEqual(1, paginated.No);
        Assert.AreEqual(10, paginated.Count);
        Assert.IsTrue(paginated.JumpToNext());
        Assert.AreEqual(2, paginated.No);
        Assert.AreEqual(10, paginated.Count);
    }

    [DataRow(1000)]
    [DataRow(2000)]
    [DataRow(3000)]
    [DataTestMethod]
    public void GetTotal(int total)
    {
        var paginated = CreatePaginatedList(total, 10);

        Assert.AreEqual(total, paginated.GetTotal());
        Assert.AreEqual(total, paginated.Total);
        Assert.IsTrue(paginated.MaxNo > 0);
    }
}
