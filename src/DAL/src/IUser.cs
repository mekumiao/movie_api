namespace MovieAPI.DAL;

/// <summary>
/// 用户信息接口
/// </summary>
public interface IUser
{
    /// <summary>
    /// 用户ID
    /// </summary>
    long Id { get; }
    /// <summary>
    /// 用户名
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 昵称
    /// </summary>
    string NickName { get; }
    /// <summary>
    /// 角色名称(如果用户拥有多个角色,则只返回第一个用角色名称)
    /// </summary>
    string Role { get; }
    /// <summary>
    /// 用户的全部角色名称
    /// </summary>
    IReadOnlyCollection<string> Roles { get; }
    /// <summary>
    /// 手机号码
    /// </summary>
    string PhoneNumber { get; }
    /// <summary>
    /// 邮箱
    /// </summary>
    string Email { get; }
    /// <summary>
    /// 性别
    /// </summary>
    Gender Gender { get; }
    /// <summary>
    /// 生日
    /// </summary>
    DateTime? BirthDate { get; }
    /// <summary>
    /// 头像
    /// </summary>
    string Picture { get; }
    /// <summary>
    /// 是否包含角色
    /// </summary>
    /// <param name="role">角色名称</param>
    /// <returns></returns>
    bool IsInRole(string role);
    /// <summary>
    /// 判断当前用户是否管理员角色
    /// </summary>
    /// <returns></returns>
    bool IsAdmin { get; }
    /// <summary>
    /// 判断当前用户是否系统角色
    /// </summary>
    /// <returns></returns>
    bool IsSystem { get; }
    /// <summary>
    /// 判断当前用户是匿名角色
    /// </summary>
    /// <returns></returns>
    bool IsAnonymous { get; }
}
