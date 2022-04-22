namespace MovieAPI.Model;

public record OperationDto
{
    public long? CreateUserId { get; set; }
    public long? UpdateUserId { get; set; }
    public DateTime? CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public string CreateUserFullName { get; set; } = string.Empty;
    public string UpdateUserFullName { get; set; } = string.Empty;
}
