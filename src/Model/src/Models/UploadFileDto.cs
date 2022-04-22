namespace MovieAPI.Model;

public partial record UploadFileDto : OperationDto
{
    public string URL { get; set; } = string.Empty;
}
