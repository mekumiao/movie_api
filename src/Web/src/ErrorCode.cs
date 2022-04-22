namespace MovieAPI.Web;

public record ErrorCode
{
    public int Code { get; set; }
    public string? Error { get; set; }
}
