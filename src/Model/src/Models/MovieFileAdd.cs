using MovieAPI.DAL;

namespace MovieAPI.Model;

public record MovieFileAdd
{
    [PathColumn]
    public string FileName { get; set; } = string.Empty;
    [PathColumn]
    public string FileFullName { get; set; } = string.Empty;
    [PathColumn]
    public string DiskURL { get; set; } = string.Empty;
}
