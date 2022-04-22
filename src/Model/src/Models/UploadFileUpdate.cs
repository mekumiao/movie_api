using MovieAPI.DAL;

namespace MovieAPI.Model;

public record UploadFileUpdate
{
    [RemarkColumn]
    public string Remark { get; set; } = string.Empty;
}
