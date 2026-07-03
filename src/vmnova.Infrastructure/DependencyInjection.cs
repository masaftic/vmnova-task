using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vmnova.Infrastructure.Data;
using vmnova.Infrastructure.Data.Seeders;

namespace vmnova.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // services.AddScoped<IAppDbContext>(serviceProvider =>
            //     serviceProvider.GetRequiredService<AppDbContext>());


            services.AddScoped<ProductSeeder>();
            services.AddScoped<CategorySeeder>();

            return services;
        }
    }

    extension(IServiceProvider services)
    {
        public async Task InitializeDatabaseAsync()
        {
            await using var scope = services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();

            var productSeeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
            await productSeeder.SeedProductsAsync();

            var categorySeeder = scope.ServiceProvider.GetRequiredService<CategorySeeder>();
            await categorySeeder.SeedCategoriesAsync();
        }
    }
}
