using System.Diagnostics;
using vmnova.Domain.Categories;
using vmnova.Domain.Constants;
using vmnova.Domain.Products;
using vmnova.Domain.Users;
using static vmnova.Domain.Constants.DefaultPermissions;

namespace vmnova.Application.Authorization;

public static class DefaultRoleFactory
{
    public static Role CreateRole(
        string roleName)
    {
        var allProductColumnPermissions = GenerateProductColumnPermission();
        var allCategoryFilterPermissions = GenerateCategoryFilterPermission();

        if (roleName == DefaultRoles.Admin)
        {
            var adminRole = Role.Create(DefaultRoles.Admin);
            var adminPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.Admin];
            adminRole.AddPermissions(allProductColumnPermissions
                .Where(p => adminPermissionsNames.Contains(p.Name)));
            adminRole.AddPermissions(allCategoryFilterPermissions
                .Where(p => adminPermissionsNames.Contains(p.Name)));

            return adminRole;
        }

        if (roleName == DefaultRoles.Sales)
        {
            var salesRole = Role.Create(DefaultRoles.Sales);
            var salesPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.Sales];
            salesRole.AddPermissions(allProductColumnPermissions
                .Where(p => salesPermissionsNames.Contains(p.Name)));
            salesRole.AddPermissions(allCategoryFilterPermissions
                .Where(p => salesPermissionsNames.Contains(p.Name)));

            return salesRole;
        }

        if (roleName == DefaultRoles.InventoryManager)
        {
            var inventoryManagerRole = Role.Create(DefaultRoles.InventoryManager);
            var inventoryManagerPermissionsNames = RolePermissionMatrix.Matrix[DefaultRoles.InventoryManager];
            inventoryManagerRole.AddPermissions(allProductColumnPermissions
                .Where(p => inventoryManagerPermissionsNames.Contains(p.Name)));
            inventoryManagerRole.AddPermissions(allCategoryFilterPermissions
                .Where(p => inventoryManagerPermissionsNames.Contains(p.Name)));

            return inventoryManagerRole;
        }

        throw new InvalidOperationException("invalid role");
    }



    private static IReadOnlyList<ColumnPermission> GenerateProductColumnPermission()
    {
        IReadOnlyList<ColumnPermission> permissions = [
            new ColumnPermission(nameof(Product), nameof(Product.Description), ProductColumnPermissions.ViewProductDescriptionColumn),
            new ColumnPermission(nameof(Product), nameof(Product.Size), ProductColumnPermissions.ViewProductSizeColumn),
            new ColumnPermission(nameof(Product), nameof(Product.WholesalePrice), ProductColumnPermissions.ViewProductWholesalePriceColumn),
            new ColumnPermission(nameof(Product), nameof(Product.SalePrice), ProductColumnPermissions.ViewProductSalePriceColumn),
        ];

        return permissions;
    }

    private static IReadOnlyList<FilterPermission> GenerateCategoryFilterPermission()
    {
        IReadOnlyList<FilterPermission> permissions = [
            new FilterPermission(nameof(Category), "CAT-01", CategoryFilterPermissions.ViewCategoryElectronics), // TODO: figure out a better what to handle these hardcoded ids
            new FilterPermission(nameof(Category), "CAT-02", CategoryFilterPermissions.ViewCategoryHomeAndKitchen),
            new FilterPermission(nameof(Category), "CAT-03", CategoryFilterPermissions.ViewCategoryApparel),
            new FilterPermission(nameof(Category), "CAT-04", CategoryFilterPermissions.ViewCategoryOfficeSupplies),
        ];

        return permissions;
    }

}
