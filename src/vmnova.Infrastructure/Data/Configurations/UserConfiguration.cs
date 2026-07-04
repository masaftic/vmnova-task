using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vmnova.Domain.Users;

namespace vmnova.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email).HasMaxLength(200);

        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<UserRole>(
                r => r.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId),
                l => l.HasOne(ur => ur.User).WithMany().HasForeignKey(ur => ur.UserId)
            );
    }
}


public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(r => r.Name)
            .IsUnique();

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>(
                r => r.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId),
                l => l.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId)
            );
    }
}


public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<Permission>("Perm")
            .HasValue<ColumnPermission>("Col") 
            .HasValue<FilterPermission>("Filter");

        builder.UseTphMappingStrategy();
    }
}
