using System.Reflection;
using vmnova.Domain.Categories;
using vmnova.Domain.Products;
using vmnova.Domain.Users;

namespace vmnova.Domain.Constants;

public static class DefaultPermissions
{
    public class ProductColumnPermissions
    {
        public const string ViewProductNameColumn = $"Products.ViewColumn.{nameof(Product.Name)}"; 
        public const string ViewProductDescriptionColumn = $"Products.ViewColumn.{nameof(Product.Description)}";
        public const string ViewProductSizeColumn = $"Products.ViewColumn.{nameof(Product.Size)}";
        public const string ViewProductCategoryIdColumn = $"Products.ViewColumn.{nameof(Product.CategoryId)}";
        public const string ViewProductWholesalePriceColumn = $"Products.ViewColumn.{nameof(Product.WholesalePrice)}";
        public const string ViewProductSalePriceColumn = $"Products.ViewColumn.{nameof(Product.SalePrice)}";
        public const string ViewProductIconSvgColumn = $"Products.ViewColumn.{nameof(Product.IconSvg)}";
    }

    public static List<string> GetAllProductPermissions()
    {
        List<string> permissions = [
            ProductColumnPermissions.ViewProductNameColumn,
            ProductColumnPermissions.ViewProductDescriptionColumn,
            ProductColumnPermissions.ViewProductSizeColumn,
            ProductColumnPermissions.ViewProductCategoryIdColumn,
            ProductColumnPermissions.ViewProductWholesalePriceColumn,
            ProductColumnPermissions.ViewProductSalePriceColumn,
            ProductColumnPermissions.ViewProductIconSvgColumn
        ];

        return permissions;
    }

    public static List<string> GetAllCategoryPermissions()
    {
        List<string> permissions = [
            CategoryFilterPermissions.ViewCategoryElectronics,
            CategoryFilterPermissions.ViewCategoryHomeAndKitchen,
            CategoryFilterPermissions.ViewCategoryApparel,
            CategoryFilterPermissions.ViewCategoryOfficeSupplies
        ];

        return permissions;
    }


    public static class CategoryFilterPermissions
    {
        // ids for seeded data
        public const string ViewCategoryElectronics = $"Categories.Filter.CAT-01"; 
        public const string ViewCategoryHomeAndKitchen = $"Categories.Filter.CAT-02";
        public const string ViewCategoryApparel = $"Categories.Filter.CAT-03";
        public const string ViewCategoryOfficeSupplies = $"Categories.Filter.CAT-04";
    }
}
