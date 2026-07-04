using vmnova.Domain.Users;

namespace vmnova.Application.Abstractions;

public interface IPermissionService
{
    Task<IReadOnlyList<Permission>> GetUserPermissions(int userId);
    Task<IReadOnlyList<Permission>> GetPermissionsByNames(string[] names);
}
