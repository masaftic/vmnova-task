using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vmnova.Application.Features.Products.GetAll;

namespace vmnova.Web.Pages;

[Authorize]
public class IndexModel(ISender sender) : PageModel
{
    public List<ProductDto> Products { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Products = await sender.Send(new GetAllProductsQuery());
    }
}
