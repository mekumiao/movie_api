namespace MovieAPI.Model;

public record Hyperlink
{
    public string Name { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
}
