using MediatR;
using vmnova.Application.Abstractions;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Logout;

public class LogoutCommandHandler(IIdentityService identityService) : IRequestHandler<LogoutCommand>
{
    public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return identityService.SignOut(cancellationToken);
    }
}
