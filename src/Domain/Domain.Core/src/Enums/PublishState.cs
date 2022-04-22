using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Domain.Core;

/// <summary>
/// 发布状态
/// </summary>
public enum PublishState : byte
{
    /// <summary>
    /// 未发布
    /// </summary>
    [Display(Name = "未发布")]
    UnPublish = 0,
    /// <summary>
    /// 已发布
    /// </summary>
    [Display(Name = "已发布")]
    Publish,
}
