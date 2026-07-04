using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using vmnova.Application.Abstractions;

namespace vmnova.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public int UserId => IsAuthenticated 
        ? int.Parse(_httpContextAccessor.HttpContext!.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        : throw new Exception("User is not authenticated");
}
