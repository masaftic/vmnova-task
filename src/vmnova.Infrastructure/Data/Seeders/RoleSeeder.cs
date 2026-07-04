using Microsoft.EntityFrameworkCore;
using vmnova.Application.Authorization;
using vmnova.Domain.Categories;
using vmnova.Domain.Constants;
using vmnova.Domain.Products;
using vmnova.Domain.Users;
using static vmnova.Domain.Constants.DefaultPermissions;

namespace vmnova.Infrastructure.Data.Seeders;

public class RoleSeeder(AppDbContext dbContext)
{
    public async Task SeedRolesAsync()
    {
        if (await dbContext.DomainRoles.AnyAsync())
            return;

        var adminRole = DefaultRoleFactory.CreateRole(DefaultRoles.Admin);
        var salesRole = DefaultRoleFactory.CreateRole(DefaultRoles.Sales);
        var inventoryManagerRole = DefaultRoleFactory.CreateRole(DefaultRoles.InventoryManager);

        List<Role> roles = [
            adminRole,
            salesRole,
            inventoryManagerRole
        ];

        dbContext.DomainRoles.AddRange(roles);
        await dbContext.SaveChangesAsync();
    }
}
