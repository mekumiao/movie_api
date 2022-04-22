using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.DAL;

/// <summary>
/// 数字列[decimal(18,5)]
/// </summary>
public class DecimalColumnAttribute : ColumnAttribute
{
    public DecimalColumnAttribute()
    {
        TypeName = "decimal(18, 5)";
    }
}
