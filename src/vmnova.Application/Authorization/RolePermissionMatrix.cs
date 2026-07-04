using vmnova.Domain.Constants;

namespace vmnova.Application.Authorization;

public static class RolePermissionMatrix
{
    public static readonly Dictionary<string, string[]> Matrix = new()
    {
        [DefaultRoles.Admin] = [
            ..DefaultPermissions.GetAllCategoryPermissions(),
            ..DefaultPermissions.GetAllProductPermissions()
        ],

        [DefaultRoles.InventoryManager] = [
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryHomeAndKitchen,
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryOfficeSupplies,

            DefaultPermissions.ProductColumnPermissions.ViewProductSizeColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductWholesalePriceColumn,
        ],

        [DefaultRoles.Sales] = [
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryElectronics,
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryApparel,

            DefaultPermissions.ProductColumnPermissions.ViewProductDescriptionColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductSizeColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductSalePriceColumn,
        ]
    };
}
