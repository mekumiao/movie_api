namespace MovieAPI.DAL;

public class HostConfig : TableEntity
{
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    [StringColumn]
    public string Host { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
}
