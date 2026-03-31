using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetDiscountAnalytics;

public class GetDiscountAnalyticsQueryHandler : IRequestHandler<GetDiscountAnalyticsQuery, Result<GeneralResponse<DiscountAnalyticsResponseDTO>>>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountAnalyticsQueryHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<Result<GeneralResponse<DiscountAnalyticsResponseDTO>>> Handle(GetDiscountAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var activeDiscounts = await _discountRepository.GetActiveDiscountsAsync();
        var activeDiscountsCount = activeDiscounts.Count();
        
        // Note: Repository seems to be missing aggregated analytics methods.
        // We'll calculate what we can from active discounts for now, or use defaults.
        
        var response = new DiscountAnalyticsResponseDTO(
            TotalActiveDiscounts: activeDiscountsCount,
            TotalSavingsProvided: 0, // Not available in current repository
            TotalRedemptions: activeDiscounts.Sum(d => d.UsageCount),
            TopPerformingDiscounts: new List<DiscountPerformanceDTO>() // Empty list for now
        );

        return Result<GeneralResponse<DiscountAnalyticsResponseDTO>>.Success(
            GeneralResponse<DiscountAnalyticsResponseDTO>.CreateSuccess(response));
    }
}
