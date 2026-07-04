using vmnova.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using vmnova.Domain.Users;
using vmnova.Infrastructure.Data;

namespace vmnova.Infrastructure.Services;

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _dbContext;

    public PermissionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Permission>> GetUserPermissions(int userId)
    {
        // join from the user to the role to the permission
        return await _dbContext.DomainUserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .SelectMany(r => r.Permissions)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Permission>> GetPermissionsByNames(string[] names)
    {
        return await _dbContext.DomainPermissions
            .Where(p => names.Contains(p.Name))
            .ToListAsync();
    }
}
