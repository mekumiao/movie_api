using System.Runtime.Serialization;

namespace MovieAPI.DAL;

public class MovieFile : TableEntity
{
    [PathColumn]
    public string FileName { get; set; } = string.Empty;
    [PathColumn]
    public string FileFullName { get; set; } = string.Empty;
    [PathColumn]
    public string DiskURL { get; set; } = string.Empty;
    [IgnoreDataMember]
    public IEnumerable<Movie>? Movies { get; set; }
}
