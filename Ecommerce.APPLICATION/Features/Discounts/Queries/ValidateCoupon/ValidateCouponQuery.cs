using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.ValidateCoupon;

public record ValidateCouponQuery(string Code, decimal OrderTotal) : IRequest<Result<CouponValidationResultDTO>>;

public record CouponValidationResultDTO(
    bool IsValid,
    string? Message,
    decimal? DiscountAmount = null
);
