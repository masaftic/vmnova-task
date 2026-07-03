using MediatR;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<Result>;
