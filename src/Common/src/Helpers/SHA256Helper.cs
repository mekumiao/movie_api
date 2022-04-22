using System.Security.Cryptography;
using System.Text;

namespace MovieAPI.Common;

public class SHA256Helper
{
    /// <summary>
    /// 将明文进行sha256 hash,得到一个大写的16进制字符串
    /// </summary>
    /// <param name="clearText!!"></param>
    /// <returns></returns>
    public static string HashToHexString(string clearText!!)
    {
        var buffer = Encoding.UTF8.GetBytes(clearText);
        buffer = SHA256.HashData(buffer);
        return Convert.ToHexString(buffer);
    }
}
