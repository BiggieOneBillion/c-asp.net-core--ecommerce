using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.CreateDiscount;

[HasPermission(Permissions.Discounts.Create)]
/// <summary>
/// Command to create a new discount or coupon
/// </summary>
/// <param name="Name">Display name for the discount</param>
/// <param name="Description">Internal or external description</param>
/// <param name="Code">Unique coupon code (leave null for automatic discounts)</param>
/// <param name="Type">Discount type (Percentage or FixedAmount)</param>
/// <param name="Value">The quantitative value of the discount</param>
/// <param name="Scope">The level at which the discount is applied (Global, Category, or Product)</param>
/// <param name="TargetId">The ID of the target product or category (required if Scope is Product or Category)</param>
/// <param name="StartDate">UTC activation date</param>
/// <param name="EndDate">UTC expiration date</param>
/// <param name="MinimumOrderAmount">Optional minimum subtotal required</param>
/// <param name="UsageLimit">Optional maximum number of redemptions allowed</param>
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
