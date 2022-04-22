using System.Runtime.Serialization;

namespace MovieAPI.DAL;

/// <summary>
/// 电影分类
/// </summary>
public class MovieType : TableEntity
{
    [StringColumn]
    public string Name { get; set; } = string.Empty;
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
    [IgnoreDataMember]
    public IEnumerable<Movie>? Movies { get; set; }
}
