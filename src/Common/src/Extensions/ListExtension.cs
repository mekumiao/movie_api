namespace MovieAPI;

public static class ListExtension
{
    /// <summary>
    /// 拆分
    /// </summary>
    /// <param name="source">要拆分的集合</param>
    /// <param name="size">拆分数</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source!!, int size)
    {
        while (source.Any())
        {
            yield return source.Take(size);
            source = source.Skip(size);
        }
    }
}
