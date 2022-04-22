using System.Runtime.Serialization;

namespace MovieAPI.DAL;

public class User : TableEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 用户姓名
    /// </summary>
    [StringColumn]
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// 用户昵称
    /// </summary>
    [StringColumn]
    public string NickName { get; set; } = string.Empty;
    /// <summary>
    /// 性别标志
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// 用户年龄
    /// </summary>
    public int Age { get; set; }
    /// <summary>
    /// 头像信息
    /// </summary>
    [PathColumn]
    public string Picture { get; set; } = string.Empty;
    /// <summary>
    /// 用户生日
    /// </summary>
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    [StringColumn]
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// 邮箱
    /// </summary>
    [StringColumn]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// 身份ID
    /// </summary>
    [StringColumn]
    public string Identity { get; set; } = string.Empty;
    /// <summary>
    /// 搜索历史
    /// </summary>
    [IgnoreDataMember]
    public ICollection<UserSearchHistory>? UserSearchHistories { get; set; }

    [IgnoreDataMember]
    public ICollection<UserMovie>? UserMovies { get; set; }

    [IgnoreDataMember]
    public ICollection<Role>? Roles { get; set; }

    [IgnoreDataMember]
    public List<RoleUser>? RoleUsers { get; set; }

    [IgnoreDataMember]
    public UserSecret? UserSecret { get; set; }
}
