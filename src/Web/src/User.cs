using IdentityModel;
using MovieAPI.Common;
using MovieAPI.DAL;

namespace MovieAPI.Web;

/// <summary>
/// 用户信息
/// </summary>
internal class User : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public User(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long Id
    {
        get
        {
            var value = GetFirstValue(JwtClaimTypes.Subject);
            if (long.TryParse(value, out long userid))
            {
                return userid;
            }
            return MyConst.User.AnonymousId;
        }
    }

    public string Name
    {
        get
        {
            var value = GetFirstValue(JwtClaimTypes.Name);
            if (Id == MyConst.User.AnonymousId)
            {
                return MyConst.User.Anonymous;
            }
            return value;
        }
    }

    public string NickName => GetFirstValue(JwtClaimTypes.NickName);

    public string Role => GetFirstValue(JwtClaimTypes.Role);

    public IReadOnlyCollection<string> Roles => GetAllValues(JwtClaimTypes.Role);

    public string PhoneNumber => GetFirstValue(JwtClaimTypes.PhoneNumber);

    public string Email => GetFirstValue(JwtClaimTypes.Email);

    public Gender Gender => Enum.TryParse<Gender>(GetFirstValue(JwtClaimTypes.Gender), out var gender) ? gender : Gender.未设置;

    public DateTime? BirthDate
    {
        get
        {
            var value = GetFirstValue(JwtClaimTypes.BirthDate);
            return DateTime.TryParse(value, out var time) ? time : null;
        }
    }

    public string Picture => GetFirstValue(JwtClaimTypes.Picture);
    /// <summary>
    /// 是否包含角色
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool IsInRole(string role) => _accessor.HttpContext?.User.IsInRole(role) ?? false;
    /// <summary>
    /// 判断当前用户是否管理员角色
    /// </summary>
    /// <returns></returns>
    public bool IsAdmin => _accessor.HttpContext?.User.IsInRole(MyConst.Role.Admin) ?? false;
    /// <summary>
    /// 判断当前用户是否系统角色
    /// </summary>
    /// <returns></returns>
    public bool IsSystem => _accessor.HttpContext?.User.IsInRole(MyConst.Role.System) ?? false;
    /// <summary>
    /// 判断当前用户是匿名用户
    /// </summary>
    /// <returns></returns>
    public bool IsAnonymous => Id == MyConst.User.AnonymousId;
    /// <summary>
    /// 根据声明类型获取值
    /// </summary>
    /// <param name="claimType">声明类型</param>
    /// <returns></returns>
    protected string GetFirstValue(string claimType)
    {
        var value = _accessor.HttpContext?.User.FindFirst(claimType)?.Value;
        return !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
    }
    /// <summary>
    /// 根据声明类型获取全部值
    /// </summary>
    /// <param name="claimType">声明类型</param>
    /// <returns></returns>
    protected string[] GetAllValues(string claimType)
    {
        var array = _accessor.HttpContext?.User.FindAll(claimType).Select(x => x.Value).ToArray();
        return array?.Any() == true ? array : Array.Empty<string>();
    }
    /// <summary>
    /// 在请求头中根据名称获取对应的值
    /// </summary>
    /// <param name="name">名称</param>
    /// <returns></returns>
    protected string GetFirstValueByHeader(string name)
    {
        var header = _accessor.HttpContext?.Request.Headers;
        if (header == default)
        {
            return string.Empty;
        }
        return header.TryGetValue(name, out var sign) ? sign : string.Empty;
    }
}
