using MediatR;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Register;

public record RegisterCommand(
    string Name,
    string Email,
    UserRoleType Role,
    string Password) : IRequest<Result>;


public enum UserRoleType // TODO: move this to a default location where permission service will populate based on the role i think
{
    Admin,
    InventoryManager,
    Sales
}
