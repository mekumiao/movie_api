namespace MovieAPI.DAL;

/// <summary>
/// 文本列[长度1000]
/// </summary>
public class TextColumnAttribute : StringColumnAttribute
{
    public TextColumnAttribute() : base(1000)
    {
    }
}
