using Microsoft.EntityFrameworkCore;

namespace MovieAPI.DAL;

public interface IPaginatedTotal
{
    public bool HasTotal { get; }
    public int No { get; }
    public int Size { get; }
    public int Total { get; }
    public int MaxNo { get; }
    public int GetTotal();
    public Task<int> GetTotalAsync();
}

/// <summary>
/// 分页的List
/// </summary>
/// <typeparam name="TSource"></typeparam>
public class PaginatedList<TSource> : List<TSource>, IPaginatedTotal where TSource : notnull
{
    private readonly IQueryable<TSource> _query;
    private int size = 10;
    private int no = 1;
    private bool isInitialize;

    /// <summary>
    /// 页大小(最小值1; 最大值500)
    /// </summary>
    public int Size
    {
        get => size;
        private set => size = value is > 0 and <= 500 ? value : size;
    }
    /// <summary>
    /// 页码(最小值1)
    /// </summary>
    public int No
    {
        get => no;
        private set => no = value >= 1 ? value : no;
    }
    /// <summary>
    /// 总数量
    /// </summary>
    public int Total { get; private set; }
    /// <summary>
    /// 是否已加载总数量
    /// </summary>
    public bool HasTotal { get; private set; }
    /// <summary>
    /// 最大页码
    /// </summary>
    public int MaxNo => (int)Math.Ceiling(Total / (double)Size);

    /// <summary>
    /// 分页的数据
    /// </summary>
    /// <param name="query"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PaginatedList(IQueryable<TSource> query!!, int no, int size)
    {
        _query = query;
        No = no;
        Size = size;
    }

    /// <summary>
    /// 加载总数量
    /// ex: 加载总数量会有额外的性能损耗。所以将该功能独立出来，指在有需要的时候手动调用
    /// </summary>
    /// <returns></returns>
    public int GetTotal()
    {
        HasTotal = true;
        return Total = _query.Count();
    }

    /// <summary>
    /// 加载总数量
    /// ex: 加载总数量会有额外的性能损耗。所以将该功能独立出来，指在有需要的时候手动调用
    /// </summary>
    /// <exception cref="OperationCanceledException"></exception>
    /// <returns></returns>
    public async Task<int> GetTotalAsync()
    {
        HasTotal = true;
        return Total = await _query.CountAsync();
    }

    /// <summary>
    /// 跳转到当前页
    /// </summary>
    /// <returns></returns>
    public bool JumpTo() => JumpTo(No);

    /// <summary>
    /// 跳转到当前页
    /// </summary>
    /// <returns></returns>
    public Task<bool> JumpToAsync() => JumpToAsync(No);

    /// <summary>
    /// 跳转到下一页
    /// </summary>
    /// <returns></returns>
    public bool JumpToNext()
    {
        if (!isInitialize)
        {
            return JumpTo();
        }
        else
        {
            return JumpTo(No + 1);
        }
    }

    /// <summary>
    /// 跳转到下一页
    /// </summary>
    /// <returns></returns>
    public Task<bool> JumpToNextAsync()
    {
        if (!isInitialize)
        {
            return JumpToAsync();
        }
        else
        {
            return JumpToAsync(No + 1);
        }
    }

    /// <summary>
    /// 跳转到页
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    public bool JumpTo(int no)
    {
        isInitialize = true;
        No = no;
        Clear();

        var items = _query.Skip((no - 1) * Size).Take(Size).ToList();
        if (items.Any())
        {
            AddRange(items);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 跳转到页
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    public async Task<bool> JumpToAsync(int no)
    {
        isInitialize = true;
        No = no;
        Clear();

        var items = await _query.Skip((no - 1) * Size).Take(Size).ToListAsync();
        if (items.Any())
        {
            AddRange(items);
            return true;
        }
        return false;
    }
}

public static class PaginatedList
{
    /// <summary>
    /// 创建PaginatedList
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static PaginatedList<TSource> Create<TSource>(
            IQueryable<TSource> source!!,
            int? no,
            int? size) where TSource : notnull
    {
        return new PaginatedList<TSource>(source, no ?? 1, size ?? 10);
    }

    /// <summary>
    /// 创建PaginatedList
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <param name="hasTotal"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static PaginatedList<TSource> CreateAfterJumpTo<TSource>(
        IQueryable<TSource> source!!,
        int? no,
        int? size,
        bool? hasTotal = null) where TSource : notnull
    {
        var data = new PaginatedList<TSource>(source, no ?? 1, size ?? 10);
        data.JumpTo();
        if (hasTotal == true)
        {
            data.GetTotal();
        }
        return data;
    }

    /// <summary>
    /// 创建PaginatedList
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="no"></param>
    /// <param name="size"></param>
    /// <param name="hasTotal"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static async Task<PaginatedList<TSource>> CreateAfterJumpToAsync<TSource>(
        IQueryable<TSource> source,
        int? no,
        int? size,
        bool? hasTotal = null) where TSource : notnull
    {
        var data = new PaginatedList<TSource>(source, no ?? 1, size ?? 10);
        await data.JumpToAsync();
        if (hasTotal == true)
        {
            await data.GetTotalAsync();
        }
        return data;
    }
}
