using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vmnova.Application.Features.Authentication.Login;
using vmnova.Domain.Shared;

namespace vmnova.Web.Pages.Account;

public class LoginModel(ISender sender) : PageModel
{
    [BindProperty]
    public LoginInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await sender.Send(
            new LoginCommand(Input.Email, Input.Password),
            cancellationToken);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return Page();
        }

        return RedirectToPage("/Index");
    }

    private void AddErrors(IEnumerable<AppError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(GetModelKey(error), error.Description);
        }
    }

    private static string GetModelKey(AppError error)
    {
        return error.Metadata?.TryGetValue("PropertyName", out var propertyName) == true
            ? $"{nameof(Input)}.{propertyName}"
            : string.Empty;
    }

    public sealed class LoginInput
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
