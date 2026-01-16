using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

/// <summary>
/// Detailed response containing discount information
/// </summary>
/// <param name="Id">Unique identifier of the discount</param>
/// <param name="Name">Display name of the discount</param>
/// <param name="Description">Description of the discount terms</param>
/// <param name="Code">Coupon code (null for automatic discounts)</param>
/// <param name="Type">Type of discount (Percentage or FixedAmount)</param>
/// <param name="Value">The discount value (e.g., 10 for 10% or 50 for $50)</param>
/// <param name="Scope">Scope of application (Global, Category, or Product)</param>
/// <param name="TargetId">The ID of the target category or product (if applicable)</param>
/// <param name="StartDate">When the discount becomes active</param>
/// <param name="EndDate">When the discount expires</param>
/// <param name="IsActive">Whether the discount is currently enabled</param>
/// <param name="MinimumOrderAmount">Minimum subtotal required to use this discount</param>
/// <param name="UsageLimit">Maximum number of times this discount can be used</param>
/// <param name="UsageCount">Current number of times this discount has been applied</param>
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
