using MediatR;
using Microsoft.EntityFrameworkCore;
using vmnova.Application.Abstractions;
using vmnova.Domain.Categories;
using vmnova.Domain.Shared;
using vmnova.Domain.Users;

namespace vmnova.Application.Features.Products.GetAll;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;

// TODO: we need a way to distinguish null properties from permission gated columns so the ui can display properly
public record ProductDto(
    string Id,
    string Name,
    string Description,
    string CategoryId,
    string CategoryName,
    string Size,
    decimal WholesalePrice,
    decimal SalePrice,
    string IconSvg);


public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
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

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetUserPermissions(_currentUser.UserId);

        var query = _dbContext.Products.AsQueryable();

        // filter by categories this user can see
        var categoryIds = permissions.OfType<FilterPermission>()
            .Where(p => p.EntityName == nameof(Category))
            .Select(p => p.EntityId)
            .ToList();

        query = query.Where(c => categoryIds.Contains(c.CategoryId));

        // TODO: project product columns

        var result = await query
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.CategoryId,
                p.Category.Name,
                p.Size,
                p.WholesalePrice,
                p.SalePrice,
                p.IconSvg
            ))
            .ToListAsync(cancellationToken);

        return result;
    }
}