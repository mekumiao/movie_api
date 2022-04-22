using System.Collections;

namespace MovieAPI.Web.Models;

public record PaginatedResult
{
    public int No { get; set; }
    public int Size { get; set; }
    public int MaxNo { get; set; }
    public int Total { get; set; }
    public IList Items { get; set; } = Array.Empty<string>();
}

public record PaginatedResult<T>
{
    public int No { get; set; }
    public int Size { get; set; }
    public int MaxNo { get; set; }
    public int Total { get; set; }
    public IList<T> Items { get; set; } = Array.Empty<T>();
}
