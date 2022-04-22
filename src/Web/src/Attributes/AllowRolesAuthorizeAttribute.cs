using Microsoft.AspNetCore.Authorization;

namespace MovieAPI.Web;

/// <summary>
/// 标记可成功通过该策略的角色
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AllowRolesAuthorizeAttribute : AuthorizeAttribute
{
    public const string POLICY_PREFIX = "allowRoles";
    /// <summary>
    /// 标记可成功通过该策略的角色
    /// </summary>
    /// <param name="roles">多个角色之间用 "," 隔开</param>
    public AllowRolesAuthorizeAttribute(string roles)
    {
        Policy = $"{POLICY_PREFIX}{roles}";
    }
}
