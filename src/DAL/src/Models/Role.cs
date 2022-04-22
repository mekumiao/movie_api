using System.Runtime.Serialization;

namespace MovieAPI.DAL;

public class Role : TableEntity
{
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    [StringColumn]
    public string DisplayName { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool IsDisabled { get; set; }

    [IgnoreDataMember]
    public ICollection<User>? Users { get; set; }

    [IgnoreDataMember]
    public List<RoleUser>? RoleUsers { get; set; }
}
