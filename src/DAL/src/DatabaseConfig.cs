namespace MovieAPI.DAL;

/// <summary>
/// 数据库配置
/// </summary>
public class DatabaseConfig
{
    /// <summary>
    /// 数据库版本
    /// </summary>
    public string Version { get; set; } = string.Empty;
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}
