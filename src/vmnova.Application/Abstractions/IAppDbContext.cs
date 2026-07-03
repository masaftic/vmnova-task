using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using vmnova.Domain.Categories;
using vmnova.Domain.Products;
using vmnova.Domain.Users;

namespace vmnova.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<User> DomainUsers { get; }
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
