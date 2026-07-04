using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vmnova.Application.Features.Products.GetAll;

namespace vmnova.Web.Pages;

[Authorize]
public class IndexModel(ISender sender) : PageModel
{
    public ProductQueryResult ProductResult { get; private set; } = null!;

    public async Task OnGetAsync()
    {
        ProductResult = await sender.Send(new GetAllProductsQuery());
    }
}
