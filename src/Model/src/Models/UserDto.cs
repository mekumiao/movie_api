namespace MovieAPI.Model;

public partial record UserDto
{
    public IEnumerable<string> RoleNames { get; set; } = Array.Empty<string>();
}
