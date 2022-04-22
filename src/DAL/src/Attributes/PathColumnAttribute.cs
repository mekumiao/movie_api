namespace MovieAPI.DAL;

/// <summary>
/// 路径列[长度500]
/// </summary>
public class PathColumnAttribute : StringColumnAttribute
{
    public PathColumnAttribute() : base(500)
    {
    }
}
