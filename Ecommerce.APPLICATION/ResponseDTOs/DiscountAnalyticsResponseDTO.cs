using System;

namespace Ecommerce.APPLICATION.ResponseDTOs;

public record DiscountAnalyticsResponseDTO(
    int TotalActiveDiscounts,
    decimal TotalSavingsProvided,
    int TotalRedemptions,
    List<DiscountPerformanceDTO> TopPerformingDiscounts
);

public record DiscountPerformanceDTO(
    Guid DiscountId,
    string Name,
    string? Code,
    int UsageCount,
    decimal TotalSavings
);
