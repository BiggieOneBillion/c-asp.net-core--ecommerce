using System;

namespace Ecommerce.APPLICATION.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasPermissionAttribute : Attribute
{
    public string Permission { get; }

    public HasPermissionAttribute(string permission)
    {
        Permission = permission;
    }
}
