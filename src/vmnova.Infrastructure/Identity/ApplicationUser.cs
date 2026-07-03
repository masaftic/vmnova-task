using Microsoft.AspNetCore.Identity;
using vmnova.Domain.Users;

namespace vmnova.Infrastructure.Identity;

// identity is used for authentication purposes
// authorization will be handled by application logic

public class ApplicationUser : IdentityUser<int>
{
    public User User { get; set; } = null!;
}


public class ApplicationRole : IdentityRole<int>
{
}