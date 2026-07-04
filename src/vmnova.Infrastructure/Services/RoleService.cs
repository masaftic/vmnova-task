using Microsoft.EntityFrameworkCore;
using vmnova.Application.Abstractions;
using vmnova.Domain.Users;
using vmnova.Infrastructure.Data;

namespace vmnova.Infrastructure.Services;

public class RoleService(AppDbContext dbContext) : IRoleService
{
    public Task<Role?> GetRoleByName(string roleName)
    {
        return dbContext.DomainRoles.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}
