using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vmnova.Infrastructure.Identity;

namespace vmnova.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<ApplicationUser>(a => a.Id); // they have the same id
    }
}
