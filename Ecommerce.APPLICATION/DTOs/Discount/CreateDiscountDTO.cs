namespace Ecommerce.APPLICATION.DTOs.Discount
{
    public record CreateDiscountDTO
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public string? CouponCode { get; init; }
        public decimal Value { get; init; }
        public string Type { get; init; } = string.Empty; // Percentage, FixedAmount
        public string Scope { get; init; } = string.Empty; // Global, Category, Product
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public bool IsActive { get; init; } = true;
        public decimal? MinimumOrderAmount { get; init; }
        public int? UsageLimit { get; init; }
        public List<Guid>? ApplicableCategoryIds { get; init; }
        public List<Guid>? ApplicableProductIds { get; init; }
    }
}
