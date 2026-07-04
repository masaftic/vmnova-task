using vmnova.Domain.Categories;
using vmnova.Domain.Products;
using vmnova.Domain.Users;
using static vmnova.Domain.Constants.DefaultPermissions;

namespace vmnova.Application.Authorization;

public static class DefaultPermissionFactory
{
    public static IReadOnlyList<Permission> CreatePermissions()
    {
        return [
            ..CreateProductColumnPermissions(),
            ..CreateCategoryFilterPermissions()
        ];
    }

    public static IReadOnlyList<ColumnPermission> CreateProductColumnPermissions()
    {
        return [
            new ColumnPermission(nameof(Product), nameof(Product.Description), ProductColumnPermissions.ViewProductDescriptionColumn),
            new ColumnPermission(nameof(Product), nameof(Product.Size), ProductColumnPermissions.ViewProductSizeColumn),
            new ColumnPermission(nameof(Product), nameof(Product.WholesalePrice), ProductColumnPermissions.ViewProductWholesalePriceColumn),
            new ColumnPermission(nameof(Product), nameof(Product.SalePrice), ProductColumnPermissions.ViewProductSalePriceColumn),
        ];
    }

    public static IReadOnlyList<FilterPermission> CreateCategoryFilterPermissions()
    {
        return [
            new FilterPermission(nameof(Category), "CAT-01", CategoryFilterPermissions.ViewCategoryElectronics),
            new FilterPermission(nameof(Category), "CAT-02", CategoryFilterPermissions.ViewCategoryHomeAndKitchen),
            new FilterPermission(nameof(Category), "CAT-03", CategoryFilterPermissions.ViewCategoryApparel),
            new FilterPermission(nameof(Category), "CAT-04", CategoryFilterPermissions.ViewCategoryOfficeSupplies),
        ];
    }

}
