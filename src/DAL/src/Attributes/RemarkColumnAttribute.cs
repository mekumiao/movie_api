namespace MovieAPI.DAL;

/// <summary>
/// 备注列[长度255]
/// </summary>
public class RemarkColumnAttribute : StringColumnAttribute
{
    public RemarkColumnAttribute() : base(255)
    {
    }
}
