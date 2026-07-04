using Microsoft.EntityFrameworkCore;
using vmnova.Application.Abstractions;
using vmnova.Application.Authorization;
using vmnova.Domain.Constants;
using vmnova.Domain.Users;

namespace vmnova.Infrastructure.Data.Seeders;

public class RoleSeeder(AppDbContext dbContext, IPermissionService permissionService)
{
    public async Task SeedRolesAsync()
    {
        if (await dbContext.DomainRoles.AnyAsync())
            return;

        var adminRole = Role.Create(DefaultRoles.Admin);
        var adminPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.Admin];
        adminRole.AddPermissions(
            await permissionService.GetPermissionsByNames(adminPermissionsNames));

        var salesRole = Role.Create(DefaultRoles.Sales);
        var salesPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.Sales];
        salesRole.AddPermissions(
            await permissionService.GetPermissionsByNames(salesPermissionsNames));

        var inventoryManagerRole = Role.Create(DefaultRoles.InventoryManager);
        var inventoryManagerPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.InventoryManager];
        inventoryManagerRole.AddPermissions(
            await permissionService.GetPermissionsByNames(inventoryManagerPermissionsNames));

        List<Role> roles = [
            adminRole,
            salesRole,
            inventoryManagerRole
        ];

        dbContext.DomainRoles.AddRange(roles);
        await dbContext.SaveChangesAsync();
    }
}
