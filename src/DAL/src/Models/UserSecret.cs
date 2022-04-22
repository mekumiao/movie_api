using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MovieAPI.Common;

namespace MovieAPI.DAL;

public class UserSecret : TableEntity
{
    public long UserId { get; set; }
    [IgnoreDataMember]
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    [StringColumn]
    public string Username { get; set; } = string.Empty;
    /// <summary>
    /// 密码使用sha256进行hash
    /// </summary>
    [RemarkColumn]
    [IgnoreDataMember]
    public string Password { get; set; } = MyConst.User.DefaultPassword;
    [StringColumn]
    [IgnoreDataMember]
    public string Salt { get; set; } = MyConst.User.DefaultSalt;
}
