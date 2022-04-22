namespace MovieAPI.Web.IntegrationTest;

public record OutputResult<T>
{
    public int Code { get; set; }
    public string? Msg { get; set; }
    public T? Result { get; set; }
}
