using MediatR;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Logout;

public record LogoutCommand : IRequest;
