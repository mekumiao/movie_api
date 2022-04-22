using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Common;

/// <summary>
/// 数字
/// </summary>
public class NumberColumnAttribute : RegularExpressionAttribute
{
    /// <summary>
    /// 数字长度
    /// </summary>
    public int Length { get; set; }
    public NumberColumnAttribute(int length) : base(@$"^[\d]{{{length}}}$")
    {
        Length = length;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"属性[{name}]的值必须是长度为[{Length}]的纯数字字符串格式";
    }
}
