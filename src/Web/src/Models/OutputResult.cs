namespace MovieAPI.Web.Models;

public record OutputResult
{
    public int Code { get; set; }
    public string? Msg { get; set; }
    public object? Result { get; set; }
}

public record OutputResult<T>
{
    public int Code { get; set; }
    public string? Msg { get; set; }
    public T? Result { get; set; }
}
