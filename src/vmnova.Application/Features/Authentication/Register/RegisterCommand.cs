using MediatR;
using vmnova.Domain.Shared;

namespace vmnova.Application.Features.Authentication.Register;

public record RegisterCommand(
    string Name,
    string Email,
    UserRoleType Role,
    string Password) : IRequest<Result>;


public enum UserRoleType
{
    Admin,
    InventoryManager,
    Sales
}
