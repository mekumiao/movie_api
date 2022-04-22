namespace MovieAPI.Web;

/// <summary>
/// 应用配置
/// </summary>
public class AppConfig
{
    /// <summary>
    /// movie的picture保存目录
    /// </summary>
    public string PictureDirectory { get; set; } = string.Empty;
    /// <summary>
    /// Apk目录
    /// </summary>
    public string ApkDirectory { get; set; } = string.Empty;
    /// <summary>
    /// 小文件上传路径
    /// </summary>
    public string UploadDirectory { get; set; } = string.Empty;
    /// <summary>
    /// 断点续传的文件保存路径
    /// </summary>
    public string TusDirectory { get; set; } = string.Empty;
    /// <summary>
    /// movie路径
    /// </summary>
    public KeyValueConfig[] Movies { get; set; } = Array.Empty<KeyValueConfig>();
    /// <summary>
    /// 最大请求body大小
    /// </summary>
    public long MaxRequestBodySize { get; set; }
}

public class KeyValueConfig
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
