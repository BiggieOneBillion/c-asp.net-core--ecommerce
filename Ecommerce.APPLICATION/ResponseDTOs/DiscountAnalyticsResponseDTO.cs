using System;

namespace Ecommerce.APPLICATION.ResponseDTOs;

/// <summary>
/// Aggregated analytics data for the discount system
/// </summary>
/// <param name="TotalActiveDiscounts">Current number of active discounts in the system</param>
/// <param name="TotalSavingsProvided">Sum of all discounts applied to completed orders</param>
/// <param name="TotalRedemptions">Cumulative number of times all discounts have been used</param>
/// <param name="TopPerformingDiscounts">List of the highest-value or most-used discounts</param>
public record DiscountAnalyticsResponseDTO(
    int TotalActiveDiscounts,
    decimal TotalSavingsProvided,
    int TotalRedemptions,
    List<DiscountPerformanceDTO> TopPerformingDiscounts
);

/// <summary>
/// Performance metrics for an individual discount
/// </summary>
/// <param name="DiscountId">Unique identifier of the discount</param>
/// <param name="Name">Name of the discount</param>
/// <param name="Code">Coupon code (if applicable)</param>
/// <param name="UsageCount">Number of times this specific discount was used</param>
/// <param name="TotalSavings">Total monetary value saved by customers using this discount</param>
public record DiscountPerformanceDTO(
    Guid DiscountId,
    string Name,
    string? Code,
    int UsageCount,
    decimal TotalSavings
);
