using vmnova.Domain.Shared;

namespace vmnova.Application.Abstractions;

public interface IIdentityService
{
    Task<Result> ValidateUser(string email, CancellationToken cancellationToken = default);
    Task<Result> CreateAuthUser(UserData userData, string password, CancellationToken cancellationToken = default);
    Task<Result> Authenticate(string email, string password, CancellationToken cancellationToken = default);
    Task<Result> SignIn(int UserId, CancellationToken cancellationToken = default);
    Task SignOut(CancellationToken cancellationToken = default);
}

public record UserData(int UserId, string Name, string Email);
