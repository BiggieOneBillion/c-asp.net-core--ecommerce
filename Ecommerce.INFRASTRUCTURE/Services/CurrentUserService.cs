using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Ecommerce.APPLICATION.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

    public string? UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public IEnumerable<string> Permissions => 
        _httpContextAccessor.HttpContext?.User?.FindAll("permission").Select(c => c.Value) ?? Enumerable.Empty<string>();

    public bool HasPermission(string permission)
    {
        return Permissions.Contains(permission);
    }
}
