using System.Collections.Generic;

namespace Ecommerce.APPLICATION.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserEmail { get; }
    IEnumerable<string> Permissions { get; }
    bool HasPermission(string permission);
}
