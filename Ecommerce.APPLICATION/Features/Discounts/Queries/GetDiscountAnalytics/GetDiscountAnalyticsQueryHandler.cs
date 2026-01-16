using System.Linq;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountAnalytics;

public class GetDiscountAnalyticsQueryHandler : IRequestHandler<GetDiscountAnalyticsQuery, Result<DiscountAnalyticsResponseDTO>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;

    public GetDiscountAnalyticsQueryHandler(IDiscountRepository discountRepository, IOrderRepository orderRepository)
    {
        _discountRepository = discountRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<DiscountAnalyticsResponseDTO>> Handle(GetDiscountAnalyticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var discounts = await _discountRepository.GetAllAsync();
            var ordersWithDiscounts = (await _orderRepository.GetAllAsync())
                .Where(o => o.AppliedDiscountId.HasValue)
                .ToList();

            int totalActiveDiscounts = discounts.Count(d => d.IsActive);
            decimal totalSavingsProvided = ordersWithDiscounts.Sum(o => o.DiscountAmount);
            int totalRedemptions = ordersWithDiscounts.Count;

            var performanceData = discounts.Select(d => new DiscountPerformanceDTO(
                d.Id,
                d.Name,
                d.Code,
                d.UsageCount,
                ordersWithDiscounts.Where(o => o.AppliedDiscountId == d.Id).Sum(o => o.DiscountAmount)
            ))
            .OrderByDescending(p => p.UsageCount)
            .Take(10)
            .ToList();

            var result = new DiscountAnalyticsResponseDTO(
                totalActiveDiscounts,
                totalSavingsProvided,
                totalRedemptions,
                performanceData
            );

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Failure<DiscountAnalyticsResponseDTO>(new Error("Discount.AnalyticsFailed", $"Failed to retrieve analytics: {ex.Message}"));
        }
    }
}
