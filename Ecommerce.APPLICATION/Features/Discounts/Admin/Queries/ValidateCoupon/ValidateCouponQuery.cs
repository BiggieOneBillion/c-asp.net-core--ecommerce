using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.ValidateCoupon;

/// <summary>
/// Query to validate a coupon code before order placement
/// </summary>
/// <param name="Code">The unique coupon code to validate</param>
/// <param name="OrderTotal">The current order subtotal to check against minimum requirements</param>
public record ValidateCouponQuery(string Code, decimal OrderTotal) : IRequest<Result<GeneralResponse<CouponValidationResultDTO>>>;

/// <summary>
/// Result of a coupon validation attempt
/// </summary>
/// <param name="IsValid">True if the coupon can be applied to the current order</param>
/// <param name="Message">Descriptive message explaining why a coupon is invalid or confirmation of validity</param>
/// <param name="DiscountAmount">The calculated discount amount that would be applied (if valid)</param>
public record CouponValidationResultDTO(
    bool IsValid,
    string? Message,
    decimal? DiscountAmount = null
);
