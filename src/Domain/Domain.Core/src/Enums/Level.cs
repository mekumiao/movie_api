using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Domain.Core;

/// <summary>
/// 级别枚举
/// </summary>
public enum Level : byte
{
    /// <summary>
    /// 初级
    /// </summary>
    [Display(Name = "初级")]
    Primary = 0,
    /// <summary>
    /// 中级
    /// </summary>
    [Display(Name = "中级")]
    Intermediate,
    /// <summary>
    /// 高级
    /// </summary>
    [Display(Name = "高级")]
    Senior,
    /// <summary>
    /// 顶级
    /// </summary>
    [Display(Name = "顶级")]
    Top,
}
