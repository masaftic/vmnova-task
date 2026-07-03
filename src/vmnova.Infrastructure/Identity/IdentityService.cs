using Microsoft.AspNetCore.Identity;
using vmnova.Application.Abstractions;
using vmnova.Domain.Shared;

namespace vmnova.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> Authenticate(string email, string password, CancellationToken cancellationToken = default)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        return result.Succeeded 
            ? Result.Ok() 
            : Result.Fail(AppError.Forbidden("Auth.InvalidCredentials", "invalid email or password"));
    }

    public async Task<Result> CreateAuthUser(UserData userData, string password, CancellationToken cancellationToken = default)
    {
        var validateRes = await ValidateUser(userData.Email, cancellationToken);
        if (validateRes.IsError) return validateRes.Errors;

        var user = new ApplicationUser
        {
            Id = userData.UserId,
            Email = userData.Email,
            UserName = userData.Email
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return result.ToAppErrors();

        return Result.Ok();
    }

    public async Task<Result> SignIn(int UserId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(UserId.ToString());
        if (user == null) return Result.Fail(AppError.NotFound("Auth.UserNotFound", "user not found"));

        await _signInManager.SignInAsync(user, false);
        return Result.Ok();
    }

    public Task SignOut(CancellationToken cancellationToken = default)
    {
        return _signInManager.SignOutAsync();
    }

    public async Task<Result> ValidateUser(string email, CancellationToken cancellationToken = default)
    {
        List<AppError> errors = []; 

        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null) errors.Add(AppError.Forbidden("Auth.EmailExists", "email already exists"));

        return errors.Any() 
            ? Result.Fail(errors) 
            : Result.Ok();
    }
}
