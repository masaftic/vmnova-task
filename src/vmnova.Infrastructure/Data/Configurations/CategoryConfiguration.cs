using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vmnova.Domain.Categories;

namespace vmnova.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(100);
        
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
