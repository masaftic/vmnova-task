using vmnova.Domain.Users;

namespace vmnova.Application.Abstractions;

public interface IRoleService
{
    Task<Role?> GetRoleByName(string roleName);
}
