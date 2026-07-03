namespace vmnova.Domain.Users;

public class RolePermission // join table
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public required Role Role { get; set; }
    public required Permission Permission { get; set; }
}
