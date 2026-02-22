using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.ValidateCoupon;

public class ValidateCouponQueryHandler : IRequestHandler<ValidateCouponQuery, Result<GeneralResponse<CouponValidationResultDTO>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IDiscountService _discountService;

    public ValidateCouponQueryHandler(IDiscountRepository discountRepository, IDiscountService discountService)
    {
        _discountRepository = discountRepository;
        _discountService = discountService;
    }

    public async Task<Result<GeneralResponse<CouponValidationResultDTO>>> Handle(ValidateCouponQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var coupon = await _discountRepository.GetByCodeAsync(request.Code);
            
            if (coupon == null)
            {
                return Result<GeneralResponse<CouponValidationResultDTO>>.Success(GeneralResponse<CouponValidationResultDTO>.CreateSuccess(new CouponValidationResultDTO(false, "Invalid coupon code")));
            }

            if (!coupon.IsActive || coupon.StartDate > DateTime.UtcNow || coupon.EndDate < DateTime.UtcNow)
            {
                return Result<GeneralResponse<CouponValidationResultDTO>>.Success(GeneralResponse<CouponValidationResultDTO>.CreateSuccess(new CouponValidationResultDTO(false, "Coupon is expired or inactive")));
            }

            if (coupon.UsageLimit.HasValue && coupon.UsageCount >= coupon.UsageLimit.Value)
            {
                return Result<GeneralResponse<CouponValidationResultDTO>>.Success(GeneralResponse<CouponValidationResultDTO>.CreateSuccess(new CouponValidationResultDTO(false, "Coupon usage limit reached")));
            }

            if (coupon.MinimumOrderAmount.HasValue && request.OrderTotal < coupon.MinimumOrderAmount.Value)
            {
                return Result<GeneralResponse<CouponValidationResultDTO>>.Success(GeneralResponse<CouponValidationResultDTO>.CreateSuccess(new CouponValidationResultDTO(false, $"Minimum order amount of {coupon.MinimumOrderAmount.Value} not met")));
            }

            // Calculate potential discount (using empty items list for global check)
            var (discountAmount, _) = await _discountService.CalculateDiscountAsync(null, request.OrderTotal, new List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)>(), request.Code);

            return Result<GeneralResponse<CouponValidationResultDTO>>.Success(GeneralResponse<CouponValidationResultDTO>.CreateSuccess(new CouponValidationResultDTO(true, "Coupon is valid", discountAmount)));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<CouponValidationResultDTO>>(new Error("Discount.ValidationFailed", $"Failed to validate coupon: {ex.Message}"));
        }
    }
}
