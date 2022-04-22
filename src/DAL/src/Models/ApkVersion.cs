using Microsoft.EntityFrameworkCore;

namespace MovieAPI.DAL;

[Index(nameof(VersionCode), IsUnique = true)]
[Index(nameof(VersionName), IsUnique = true)]
public class ApkVersion : TableEntity
{
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    public int VersionCode { get; set; }
    [StringColumn]
    public string VersionName { get; set; } = string.Empty;
    [PathColumn]
    public string FileDiskURL { get; set; } = string.Empty;
    [TextColumn]
    public string Remark { get; set; } = string.Empty;
    public bool IsActived { get; set; }
    public int Downloads { get; set; }
}
