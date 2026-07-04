using MediatR;
using Microsoft.EntityFrameworkCore;
using vmnova.Application.Abstractions;
using vmnova.Domain.Categories;
using vmnova.Domain.Products;
using vmnova.Domain.Shared;
using vmnova.Domain.Users;

namespace vmnova.Application.Features.Products.GetAll;

public record GetAllProductsQuery : IRequest<ProductQueryResult>;

public record ProductQueryResult(
    List<ProductColumn> VisibleColumns,
    List<ProductDto> Products);

public record ProductDto(
    string Id,
    string Name,
    string? Description,
    string CategoryId,
    string CategoryName,
    string? Size,
    decimal? WholesalePrice,
    decimal? SalePrice,
    string IconSvg);

public enum ProductColumn
{
    Description,
    Size,
    WholesalePrice,
    SalePrice
}


public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductQueryResult>
{
    private readonly IPermissionService _permissionService;
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;

    public GetAllProductsQueryHandler(IPermissionService permissionService, IAppDbContext dbContext, ICurrentUserService currentUser)
    {
        _permissionService = permissionService;
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<ProductQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetUserPermissions(_currentUser.UserId);

        var query = _dbContext.Products.AsQueryable();

        // filter by categories this user can see
        var categoryIds = permissions.OfType<FilterPermission>()
            .Where(p => p.EntityName == nameof(Category))
            .Select(p => p.EntityId)
            .ToList();

        query = query.Where(c => categoryIds.Contains(c.CategoryId));


        var productColumns = permissions.OfType<ColumnPermission>()
            .Where(p => p.EntityName == nameof(Product))
            .ToList();

        var canSeeDescription = productColumns.Any(p => p.ColumnName == nameof(Product.Description));
        var canSeeSize = productColumns.Any(p => p.ColumnName == nameof(Product.Size));
        var canSeeWholesalePrice = productColumns.Any(p => p.ColumnName == nameof(Product.WholesalePrice));
        var canSeeSalePrice = productColumns.Any(p => p.ColumnName == nameof(Product.SalePrice));

        var result = await query
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                canSeeDescription ? p.Description : null,
                p.CategoryId,
                p.Category.Name,
                canSeeSize ? p.Size : null,
                canSeeWholesalePrice ? p.WholesalePrice : null,
                canSeeSalePrice ? p.SalePrice : null,
                p.IconSvg
            ))
            .ToListAsync(cancellationToken);

        var visibleColumns = productColumns.Select(p => Enum.Parse<ProductColumn>(p.ColumnName)).ToList();

        return new ProductQueryResult(
            visibleColumns,
            result
        );
    }
}