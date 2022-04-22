namespace MovieAPI.Common;

/// <summary>
/// 常量选项
/// </summary>
public static class MyConst
{
    /// <summary>
    /// 预设角色常量
    /// </summary>
    public static class Role
    {
        /// <summary>
        /// 系统角色(表示系统本身)
        /// </summary>
        public const string System = "system";
        public const long SystemId = 101;
        /// <summary>
        /// 管理员角色
        /// </summary>
        public const string Admin = "admin";
        public const long AdminId = 102;
        /// <summary>
        /// 普通用户
        /// </summary>
        public const string User = "user";
        public const long UserId = 105;
        /// <summary>
        /// 匿名角色
        /// </summary>
        public const string Anonymous = "anonymous";
        public const long AnonymousId = 50;
    }
    /// <summary>
    /// 预设用户常量
    /// </summary>
    public static class User
    {
        /// <summary>
        /// 系统(表示系统本身)
        /// </summary>
        public const string System = "system";
        public const long SystemId = 101;
        /// <summary>
        /// 管理员
        /// </summary>
        public const string Admin = "admin";
        public const long AdminId = 102;
        /// <summary>
        /// 用户
        /// </summary>
        public const string MyUser = "user";
        public const long MyUserId = 105;
        /// <summary>
        /// 匿名
        /// </summary>
        public const string Anonymous = "anonymous";
        public const long AnonymousId = 50;
        /// <summary>
        /// 默认密码
        /// </summary>
        /// <remark>
        /// 计算规则：sha256({salt}{password})。示例：sha256(wangsir123123)
        /// </remark>
        public const string DefaultPassword = "017B509333DCF2C50E26D6214BDE0DBF9DD227B81216FBAFC61CAC4451AE0ECF";
        public const string DefaultSalt = "wangsir";
    }
}
