namespace MovieAPI.Model;

public partial record MovieDto
{
    public string PictureURL { get; set; } = string.Empty;
    public string MovieFileURL { get; set; } = string.Empty;
    public bool IsStar { get; set; }
    public bool IsDislike { get; set; }
}
