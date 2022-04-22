using System.Diagnostics.CodeAnalysis;

namespace MovieAPI.Common;

public class SortingList : List<Sorting>
{
    public SortingList() { }

    public SortingList(IEnumerable<Sorting> collection) : base(collection) { }

    private static IEnumerable<Sorting> RemoveNullItem(IEnumerable<Sorting?> items)
    {
        foreach (var item in items)
        {
            if (item is not null)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// 将排序字符串转为Sorting[]
    /// </summary>
    /// <param name="sortingString">
    /// + 格式:
    /// 1. {field} {order}
    /// 2. {field}表示字段名, {order}表示排序方式,对应的值为: [desc,asc]
    /// 3. 对多个字段排序请使用','拼接
    /// + 例如:
    /// 1. name asc
    /// 2. name desc,time asc
    /// </param>
    /// <param name="sortings"></param>
    /// <returns></returns>
    public static bool TryParse(string? sortingString, [NotNullWhen(true)] out SortingList? sortings)
    {
        sortings = null;
        if (sortingString is not null && !string.IsNullOrWhiteSpace(sortingString))
        {
            var array = sortingString.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (array.Any())
            {
                sortings = new SortingList(RemoveNullItem(array.Select(x =>
                {
                    var sp = x.Split(' ');
                    if (sp.Length > 0)
                    {
                        var sort = new Sorting { Field = sp[0].Trim(), };
                        if (sp.Length > 1)
                        {
                            if (sp[1].Trim() == "desc")
                            {
                                sort.IsDescending = true;
                            }
                        }
                        return sort;
                    }
                    return default;
                })));
                return sortings.Any();
            }
        }
        return false;
    }

}
