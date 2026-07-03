using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using vmnova.Domain.Categories;

namespace vmnova.Infrastructure.Data.Seeders;

public class CategorySeeder(AppDbContext dbContext)
{
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public async Task SeedCategoriesAsync()
    {
        if (await dbContext.Categories.AnyAsync())
            return;

        var path = Path.Combine(AppContext.BaseDirectory, "Data", "Seeders", "Data", "categories.json");

        var data = await File.ReadAllTextAsync(path);

        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(data, options) ?? throw new InvalidOperationException("Seed data not found");

        await dbContext.AddRangeAsync(categories.Select(p => p.ToCategory()));
    }

    public class CategoryDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string IconSvg { get; set; } = null!;

        public Category ToCategory() => Category.Create(Id, Name, IconSvg);
    }
}
