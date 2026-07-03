using Microsoft.AspNetCore.Identity;
using vmnova.Domain.Shared;

namespace vmnova.Infrastructure.Identity;

public static class IdentityExtensions
{
    extension(IdentityResult result)
    {
        public List<AppError> ToAppErrors()
            => result.Errors.Select(Map).ToList();
    }

    private static AppError Map(IdentityError error)
        => error.Code switch
        {
            "DuplicateEmail" or "DuplicateUserName" => AppError.Conflict(
                "Auth.EmailExists",
                error.Description),

            _ => AppError.Validation(
                code: $"Identity.{error.Code}",
                description: error.Description).WithMetadata("code", error.Code),
        };
}
