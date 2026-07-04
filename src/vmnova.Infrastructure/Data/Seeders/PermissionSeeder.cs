using Microsoft.EntityFrameworkCore;
using vmnova.Application.Authorization;

namespace vmnova.Infrastructure.Data.Seeders;

public class PermissionSeeder(AppDbContext dbContext)
{
    public async Task SeedPermissionsAsync()
    {
        if (await dbContext.DomainPermissions.AnyAsync())
            return;
        
        var permissions = DefaultPermissionFactory.CreatePermissions();
        dbContext.DomainPermissions.AddRange(permissions);
        await dbContext.SaveChangesAsync();
    } 
}
