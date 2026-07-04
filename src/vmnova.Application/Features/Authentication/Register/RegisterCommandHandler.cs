using MediatR;
using vmnova.Application.Abstractions;
using vmnova.Application.Authorization;
using vmnova.Domain.Shared;
using vmnova.Domain.Users;

namespace vmnova.Application.Features.Authentication.Register;

public class RegisterCommandHandler(
    IIdentityService identityService,
    IRoleService roleService,
    IAppDbContext dbContext) : IRequestHandler<RegisterCommand, Result>
{
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validateRes = await identityService.ValidateUser(request.Email, cancellationToken);
        if (validateRes.IsError)
            return validateRes.Errors;

        using var trx = await dbContext.BeginTransactionAsync(cancellationToken);

        var user = User.Create(request.Name, request.Email);
        var role = await roleService.GetRoleByName(request.Role.ToString());

        if (role is null)
            return AppError.NotFound("Auth.RoleNotFound", "role not found");

        user.AddRole(role);

        dbContext.DomainUsers.Add(user); // save to get the user id because identity requires it
        await dbContext.SaveChangesAsync(cancellationToken);

        var res = await identityService.CreateAuthUser(new UserData(user.Id, user.Name, user.Email), request.Password, cancellationToken);
        if (res.IsError)
            return res.Errors;

        await trx.CommitAsync(cancellationToken);

        await identityService.SignIn(user.Id, cancellationToken);

        return Result.Ok();
    }
}
