using System.Collections.Generic;
using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class PermissionProvider : IPermissionProvider
{
    private static readonly Dictionary<UserRole, HashSet<string>> _rolePermissions = new()
    {
        [UserRole.Admin] = new HashSet<string>
        {
            Permissions.Users.View, Permissions.Users.Edit, Permissions.Users.Delete,
            Permissions.Products.View, Permissions.Products.Search, Permissions.Products.Create, Permissions.Products.Update, Permissions.Products.Delete,
            Permissions.Categories.View, Permissions.Categories.Create, Permissions.Categories.Update, Permissions.Categories.Delete,
            Permissions.Inventory.View, Permissions.Inventory.Manage,
            Permissions.Orders.ViewAll, Permissions.Orders.ViewOwn, Permissions.Orders.Create, Permissions.Orders.UpdateStatus, Permissions.Orders.Refund,
            Permissions.Payments.ViewAll, Permissions.Payments.ViewOwn, Permissions.Payments.Process
        },
        [UserRole.Staff] = new HashSet<string>
        {
            Permissions.Products.View, Permissions.Products.Search,
            Permissions.Categories.View,
            Permissions.Inventory.View, Permissions.Inventory.Manage,
            Permissions.Orders.ViewAll, Permissions.Orders.ViewOwn, Permissions.Orders.UpdateStatus,
            Permissions.Payments.ViewAll, Permissions.Payments.ViewOwn
        },
        [UserRole.Customer] = new HashSet<string>
        {
            Permissions.Products.View, Permissions.Products.Search,
            Permissions.Categories.View,
            Permissions.Orders.ViewOwn, Permissions.Orders.Create,
            Permissions.Payments.ViewOwn, Permissions.Payments.Process
        }
    };

    public HashSet<string> GetPermissionsForRole(UserRole role)
    {
        return _rolePermissions.TryGetValue(role, out var permissions) 
            ? permissions 
            : new HashSet<string>();
    }
}
