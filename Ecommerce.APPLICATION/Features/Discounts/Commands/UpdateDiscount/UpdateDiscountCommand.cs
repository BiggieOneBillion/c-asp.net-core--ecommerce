using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;

[HasPermission(Permissions.Users.View)] // Placeholder
public record UpdateDiscountCommand(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive
) : ICommand<Unit>;
