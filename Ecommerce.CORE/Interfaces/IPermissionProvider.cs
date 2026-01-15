using System.Collections.Generic;
using Ecommerce.CORE.Enums;

namespace Ecommerce.CORE.Interfaces;

public interface IPermissionProvider
{
    HashSet<string> GetPermissionsForRole(UserRole role);
}
