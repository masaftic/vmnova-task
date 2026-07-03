using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using vmnova.Domain.Products;

namespace vmnova.Infrastructure.Data.Seeders;

public class ProductSeeder(AppDbContext dbContext)
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    public async Task SeedProductsAsync()
    {
        if (await dbContext.Products.AnyAsync())
            return;

        var path = Path.Combine(AppContext.BaseDirectory, "Data", "Seeders", "Data", "products.json");

        var data = await File.ReadAllTextAsync(path);

        var products = JsonSerializer.Deserialize<List<ProductDto>>(data, options) ?? throw new InvalidOperationException("Seed data not found");

        await dbContext.AddRangeAsync(products.Select(p => p.ToProduct()));
    }

    public class ProductDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string Size { get; set; } = null!;
        public decimal WholesalePrice { get; set; }
        public decimal SalePrice { get; set; }
        public string IconSvg { get; set; } = null!;

        public Product ToProduct() => Product.Create(Id, Name, Description, CategoryId, Size, WholesalePrice, SalePrice, IconSvg);
    }
}
