using MediatR;
using vmnova.Application.Abstractions;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Login;

public class LoginCommandHandler(IIdentityService identityService) : IRequestHandler<LoginCommand, Result>
{
    public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.Authenticate(
            request.Email,
            request.Password,
            cancellationToken);

        return result;
    }
}
