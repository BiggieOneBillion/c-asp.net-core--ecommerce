using Ecommerce.CORE.Constants;
using Ecommerce.CORE.Enums;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Commands.CreateDiscount;

/// <summary>
/// Command to create a new discount or coupon
/// </summary>
/// <param name="Name">Display name for the discount</param>
/// <param name="Description">Internal or external description</param>
/// <param name="CouponCode">Unique coupon code (leave null for automatic discounts)</param>
/// <param name="Value">The quantitative value of the discount</param>
/// <param name="Type">Discount type (Percentage or FixedAmount)</param>
/// <param name="Scope">The level at which the discount is applied (Global, Category, or Product)</param>
/// <param name="StartDate">UTC activation date</param>
/// <param name="EndDate">UTC expiration date</param>
/// <param name="IsActive">Indicates if the discount is currently active</param>
/// <param name="MinimumOrderAmount">Optional minimum subtotal required</param>
/// <param name="UsageLimit">Optional maximum number of redemptions allowed</param>
/// <param name="ApplicableCategoryIds">List of category IDs if Scope is Category</param>
/// <param name="ApplicableProductIds">List of product IDs if Scope is Product</param>
public record CreateDiscountCommand(
    string Name,
    string? Description,
    string? CouponCode,
    decimal Value,
    string Type, // Percentage, FixedAmount
    string Scope, // Global, Category, Product
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive = true,
    decimal? MinimumOrderAmount = null,
    int? UsageLimit = null,
    List<Guid>? ApplicableCategoryIds = null,
    List<Guid>? ApplicableProductIds = null
) : IRequest<Result<GeneralResponse<Guid>>>;
