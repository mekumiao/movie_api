using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Domain.Core;

/// <summary>
/// 面积单位枚举
/// </summary>
public enum AreaUnit : byte
{
    /// <summary>
    /// 平方毫米
    /// </summary>
    [Display(Name = "平方毫米")]
    MM = 0,
    /// <summary>
    /// 平方厘米
    /// </summary>
    [Display(Name = "平方厘米")]
    CM,
    /// <summary>
    /// 平方分米
    /// </summary>
    [Display(Name = "平方分米")]
    DM,
    /// <summary>
    /// 平方米
    /// </summary>
    [Display(Name = "平方米")]
    M,
    /// <summary>
    /// 公顷
    /// </summary>
    [Display(Name = "公顷")]
    HM,
    /// <summary>
    /// 平方千米
    /// </summary>
    [Display(Name = "平方千米")]
    KM,
}
