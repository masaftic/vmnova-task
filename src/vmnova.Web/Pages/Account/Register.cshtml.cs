using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vmnova.Application.Features.Authentication.Register;
using vmnova.Domain.Shared;

namespace vmnova.Web.Pages.Account;

public class RegisterModel(ISender sender) : PageModel
{
    [BindProperty]
    public RegisterInput Input { get; set; } = new();

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
            new RegisterCommand(Input.Name, Input.Email, Input.Role, Input.Password),
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

    public sealed class RegisterInput
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRoleType Role { get; set; } = UserRoleType.Sales;
        public string Password { get; set; } = string.Empty;
    }
}
