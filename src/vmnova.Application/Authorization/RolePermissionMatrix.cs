using vmnova.Domain.Constants;

namespace vmnova.Application.Authorization;

public static class RolePermissionMatrix
{
    public static readonly Dictionary<string, List<string>> Matrix = new()
    {
        [DefaultRoles.Admin] = [
            ..DefaultPermissions.GetAllCategoryPermissions(),
            ..DefaultPermissions.GetAllProductPermissions()
        ],

        [DefaultRoles.InventoryManager] = [
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryHomeAndKitchen,
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryOfficeSupplies,

            DefaultPermissions.ProductColumnPermissions.ViewProductNameColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductCategoryIdColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductSizeColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductWholesalePriceColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductIconSvgColumn
        ],

        [DefaultRoles.Sales] = [
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryElectronics,
            DefaultPermissions.CategoryFilterPermissions.ViewCategoryApparel,

            DefaultPermissions.ProductColumnPermissions.ViewProductNameColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductDescriptionColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductCategoryIdColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductSizeColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductSalePriceColumn,
            DefaultPermissions.ProductColumnPermissions.ViewProductIconSvgColumn
        ]
    };
}
