using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Common;

/// <summary>
/// 包含数字和单词
/// </summary>
public class NumberWithWordColumnAttribute : RegularExpressionAttribute
{
    public int Length { get; set; }
    public NumberWithWordColumnAttribute(int length) : base(@$"^[\d\w]{{{length}}}$")
    {
        Length = length;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"属性[{name}]的值必须是长度为[{Length}]的数字或字母格式";
    }
}
