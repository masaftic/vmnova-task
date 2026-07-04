using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using vmnova.Application.Abstractions;
using vmnova.Domain.Categories;
using vmnova.Domain.Products;
using vmnova.Domain.Users;
using vmnova.Infrastructure.Identity;

namespace vmnova.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options), IAppDbContext
{
    public DbSet<User> DomainUsers { get; set; }
    public DbSet<UserRole> DomainUserRoles { get; set; }
    public DbSet<Role> DomainRoles { get; set; }
    public DbSet<Permission> DomainPermissions { get; set; }
    public DbSet<RolePermission> DomainRolePermissions { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
