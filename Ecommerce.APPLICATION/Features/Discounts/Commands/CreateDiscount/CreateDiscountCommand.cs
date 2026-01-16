using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.CreateDiscount;

[HasPermission(Permissions.Users.View)] // Assuming management requires high permission, using a placeholder for now
public record CreateDiscountCommand(
    string Name,
    string? Description,
    string? Code,
    DiscountType Type,
    decimal Value,
    DiscountScope Scope,
    Guid? TargetId,
    DateTime StartDate,
    DateTime EndDate,
    decimal? MinimumOrderAmount,
    int? UsageLimit
) : ICommand<Guid>;
