using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;

[HasPermission(Permissions.Discounts.Update)]
/// <summary>
/// Command to update descriptive metadata or status of an existing discount
/// </summary>
/// <param name="Id">The unique identifier of the discount to update</param>
/// <param name="Name">New display name</param>
/// <param name="Description">New description</param>
/// <param name="IsActive">Whether the discount should be enabled or disabled</param>
public record UpdateDiscountCommand(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive
) : ICommand<Unit>;
