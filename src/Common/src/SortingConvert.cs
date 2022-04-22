using System.Linq.Expressions;

namespace MovieAPI.Common;

/// <summary>
/// 查询条件转换类
/// </summary>
public class SortingConvert
{
    private readonly static ReaderWriterLockSlim rwl = new();
    private readonly static Dictionary<string, HashSet<string>> _fieldCache = new();

    /// <summary>
    /// 获取类型的属性集合
    /// </summary>
    /// <param name="type"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <returns></returns>
    private static HashSet<string> GetTypePropNames(Type type!!)
    {
        if (string.IsNullOrWhiteSpace(type.FullName))
        {
            throw new ArgumentException("类型名称不能为空白", nameof(type));
        }

        if (rwl.TryEnterReadLock(500))
        {
            try
            {
                if (_fieldCache.TryGetValue(type.FullName, out var set))
                {
                    return set;
                }
                rwl.ExitReadLock();
                if (rwl.TryEnterWriteLock(1000))
                {
                    try
                    {
                        if (!_fieldCache.TryGetValue(type.FullName, out set))
                        {
                            var props = type.GetProperties()
                                 .Where(x => x.CanWrite && x.CanRead)
                                 .Where(x => IsTypesSupported(x.PropertyType))
                                 .Select(x => x.Name);
                            set = _fieldCache[type.FullName] = new HashSet<string>(props, StringComparer.InvariantCultureIgnoreCase);
                        }
                        return set;
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
                throw new InvalidOperationException("并发过高,请稍后重试");
            }
            finally
            {
                if (rwl.IsReadLockHeld)
                {
                    rwl.ExitReadLock();
                }
            }
        }
        throw new InvalidOperationException("并发过高,请稍后重试");
    }

    /// <summary>
    /// 判断属性是否符合要求
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="propName">属性名称</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    private static bool IsConform<TEntity>(string propName)
        where TEntity : class, new()
    {
        if (string.IsNullOrWhiteSpace(propName))
        {
            throw new ArgumentException($"“{nameof(propName)}”不能为 null 或空白。", nameof(propName));
        }

        return GetTypePropNames(typeof(TEntity)).Contains(propName);
    }

    /// <summary>
    /// 判断是否是支持的类型
    /// </summary>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    private static bool IsTypesSupported(Type type!!)
    {
        if (type.IsEnum)
        {
            return true;
        }
        var typeName = type.Name;
        if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            typeName = type.GetGenericArguments()[0].Name;
        }
        return typeName switch
        {
            nameof(Int16) => true,
            nameof(Int32) => true,
            nameof(Int64) => true,
            nameof(UInt16) => true,
            nameof(UInt32) => true,
            nameof(UInt64) => true,
            nameof(Byte) => true,
            nameof(SByte) => true,
            nameof(Double) => true,
            nameof(Single) => true,
            nameof(Decimal) => true,
            nameof(Boolean) => true,
            nameof(Char) => true,
            nameof(DateTime) => true,
            nameof(DateTimeOffset) => true,
            nameof(String) => true,
            _ => false,
        };
    }

    /// <summary>
    /// 尝试添加排序
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sorting"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static bool TryAddSortingsTo<TEntity>(ref IQueryable<TEntity> source, string? sorting)
        where TEntity : class, new()
    {
        if (SortingList.TryParse(sorting, out var sortings))
        {
            source = AddSortingsTo(source, sortings);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 添加排序
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="sortings"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static IQueryable<TEntity> AddSortingsTo<TEntity>(IQueryable<TEntity> source!!, SortingList sortings!!)
        where TEntity : class, new()
    {
        if (!sortings.Any())
        {
            return source;
        }
        if (!IsConform<TEntity>(sortings[0].Field))
        {
            return source;
        }
        var px = Expression.Parameter(typeof(TEntity), "px");
        var queryExpression = source.Expression;
        var selector = Expression.Property(px, sortings[0].Field);
        queryExpression = CallOrderBy(true, sortings[0].IsDescending) ?? source.Expression;
        foreach (var item in sortings.Skip(1))
        {
            if (!IsConform<TEntity>(item.Field))
            {
                break;
            }
            selector = Expression.Property(px, item.Field);
            queryExpression = CallOrderBy(false, item.IsDescending) ?? queryExpression;
        }
        return source.Provider.CreateQuery<TEntity>(queryExpression);

        Expression? CallOrderBy(bool first, bool desc)
        {
            return (first, desc) switch
            {
                (true, true) => CreateMethodCall(nameof(Queryable.OrderByDescending)),
                (true, false) => CreateMethodCall(nameof(Queryable.OrderBy)),
                (false, true) => CreateMethodCall(nameof(Queryable.ThenByDescending)),
                (false, false) => CreateMethodCall(nameof(Queryable.ThenBy))
            };
        }
        MethodCallExpression CreateMethodCall(string methodName)
        {
            return Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { source.ElementType, selector!.Type },
                queryExpression,
                Expression.Quote(Expression.Lambda(selector, px)));
        }
    }

}
