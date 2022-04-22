using System.Diagnostics;

namespace MovieAPI.Common;

public class NETAaptHelper
{
    /// <summary>
    /// 获取apk版本信息
    /// </summary>
    /// <param name="apkPath"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    public static ApkInfo? GetApkInfo(string apkPath)
    {
        if (string.IsNullOrWhiteSpace(apkPath))
        {
            throw new ArgumentException($"“{nameof(apkPath)}”不能为 null 或空白。", nameof(apkPath));
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = Path.Combine(AppContext.BaseDirectory, "aapt2.exe"),
            Arguments = $"dump badging {apkPath}",
            UseShellExecute = false,
            CreateNoWindow = false,
            RedirectStandardOutput = true,
            StandardOutputEncoding = System.Text.Encoding.UTF8,
        };

        string? result = null;

        try
        {
            using var process = Process.Start(startInfo);
            result = process?.StandardOutput.ReadLine();
        }
        catch (Exception)
        {
            return null;
        }

        var list = result?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (list?.Length > 0)
        {
            var apk = new ApkInfo
            {
                Name = list[1][6..^1],
                VersionCode = int.Parse(list[2][13..^1]),
                VersionName = list[3][13..^1],
            };
            return apk;
        }

        return null;
    }
}

public class ApkInfo
{
    public string Name { get; set; } = string.Empty;
    public string VersionName { get; set; } = string.Empty;
    public int VersionCode { get; set; }
}
