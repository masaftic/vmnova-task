namespace vmnova.Domain.Users;

public class UserRole // just a join table
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public required User User { get; set; }
    public required Role Role { get; set; }
}
