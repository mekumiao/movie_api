using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MovieAPI.DAL;

/// <summary>
/// 字符串列[长度40]
/// </summary>
public class StringColumnAttribute : StringLengthAttribute
{
    public StringColumnAttribute() : base(40) { }

    public StringColumnAttribute(int length) : base(length) { }

    public override string FormatErrorMessage(string name)
    {
        if (MinimumLength > 0)
        {
            return $"属性[{name}]的值长度应在[{MinimumLength}~{MaximumLength}]之间";
        }
        return $"属性[{name}]的值长度不应超过[{MaximumLength}]";
    }

    [return: NotNullIfNotNull("value")]
    public static string? EnsureOkLength(string? value, int minimumLength = 40)
    {
        if (value is not null)
        {
            if (value.Length > minimumLength)
            {
                value = value[^minimumLength..];
            }
            return value;
        }
        return value;
    }
}
