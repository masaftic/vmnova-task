using vmnova.Domain.Categories;

namespace vmnova.Domain.Products;

public class Product
{
    public string Id { get; init; } = null!;
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string CategoryId { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
    public string Size { get; private set; } = null!;
    public decimal WholesalePrice { get; private set; }
    public decimal SalePrice { get; private set; }
    public string IconSvg { get; private set; } = null!;

    private Product() { }

    public static Product Create(string id, string name, string description, string categoryId, string size, decimal wholesalePrice, decimal salePrice, string iconSvg)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(size);
        
        if (wholesalePrice < 0 || salePrice < 0)
            throw new ArgumentException("Price cannot be negative");

        return new Product
        {
            Id = id,
            Name = name.Trim(),
            Description = description.Trim(),
            CategoryId = categoryId,
            Size = size.Trim(),
            WholesalePrice = wholesalePrice,
            SalePrice = salePrice,
            IconSvg = iconSvg
        };
    }

    public void Update(string name, string description, string categoryId, string size, decimal wholesalePrice, decimal salePrice, string iconSvg)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(size);
        
        if (wholesalePrice < 0 || salePrice < 0)
            throw new ArgumentException("Price cannot be negative");

        Name = name.Trim();
        Description = description.Trim();
        CategoryId = categoryId;
        Size = size.Trim();
        WholesalePrice = wholesalePrice;
        SalePrice = salePrice;
        IconSvg = iconSvg;
    }
}
