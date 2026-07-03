using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vmnova.Application.Abstractions;
using vmnova.Infrastructure.Data;
using vmnova.Infrastructure.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using vmnova.Infrastructure.Identity;

namespace vmnova.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAppDbContext>(serviceProvider =>
                serviceProvider.GetRequiredService<AppDbContext>());

            services.AddScoped<ProductSeeder>();
            services.AddScoped<CategorySeeder>();

            services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 3;

                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAuthentication(defaultScheme: IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();
                

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

            var categorySeeder = scope.ServiceProvider.GetRequiredService<CategorySeeder>();
            await categorySeeder.SeedCategoriesAsync();

            var productSeeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
            await productSeeder.SeedProductsAsync();
        }
    }
}
