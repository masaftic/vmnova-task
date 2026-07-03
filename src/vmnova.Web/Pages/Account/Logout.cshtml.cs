using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vmnova.Application.Features.Authentication.Logout;
using vmnova.Domain.Shared;

namespace vmnova.Web.Pages.Account;

public class LogoutModel(ISender sender) : PageModel
{
    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await sender.Send(
            new LogoutCommand(),
            cancellationToken);

        return RedirectToPage("/Index");
    }
}
