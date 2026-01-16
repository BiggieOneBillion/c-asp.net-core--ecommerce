using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

public record DiscountResponseDTO(
    Guid Id,
    string Name,
    string? Description,
    string? Code,
    DiscountType Type,
    decimal Value,
    DiscountScope Scope,
    Guid? TargetId,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive,
    decimal? MinimumOrderAmount,
    int? UsageLimit,
    int UsageCount
);
