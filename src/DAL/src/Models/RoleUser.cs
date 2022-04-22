namespace MovieAPI.DAL;

public class RoleUser : ITable
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public User? User { get; set; }
    public Role? Role { get; set; }
}
