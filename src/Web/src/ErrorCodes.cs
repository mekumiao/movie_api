using System.ComponentModel;

namespace MovieAPI.Web;

public static partial class ErrorCodes
{
    /// <summary>
    /// 条目不存在或已被删除
    /// </summary>
    [Description("条目不存在或已被删除")]
    public const int NotExists = 1;
    /// <summary>
    /// 条目已经存在
    /// </summary>
    [Description("条目已经存在")]
    public const int AlreadyExists = 2;
    /// <summary>
    /// 密码错误
    /// </summary>
    [Description("密码错误")]
    public const int PasswordError = 3;
    /// <summary>
    /// 用户名不存在
    /// </summary>
    [Description("用户名不存在")]
    public const int UsernameNoExists = 4;
    /// <summary>
    /// 系统繁忙(一般用在由并发过高导致的错误或者意外情况)
    /// </summary>
    [Description("系统繁忙")]
    public const int SystemBusy = 100;
}
