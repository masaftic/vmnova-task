using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vmnova.Domain.Products;

namespace vmnova.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Size)
            .HasMaxLength(50);

        builder.Property(p => p.WholesalePrice)
            .HasPrecision(18, 2);

        builder.Property(p => p.SalePrice)
            .HasPrecision(18, 2);
        
        builder.Property(p => p.IconSvg)
            .HasMaxLength(1000);
        
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
