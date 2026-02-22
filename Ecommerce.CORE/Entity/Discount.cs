using Ecommerce.CORE.Common;
using Ecommerce.CORE.Enums;

namespace Ecommerce.CORE.Entity;

public class Discount : AggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Code { get; set; } // Coupon code (null for automatic discounts applied at product/category level)
    
    public DiscountType Type { get; set; }
    public decimal Value { get; set; } // Percentage (e.g., 10 for 10%) or Fixed amount (e.g., 50.00)
    
    public DiscountScope Scope { get; set; }
    public Guid? TargetId { get; set; } // Associated ProductId, CategoryId, or UserId based on Scope
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public decimal? MinimumOrderAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;

    // Private constructor for EF Core
    private Discount() { }

    // Factory method
    public static Discount Create(
        string name, 
        string? description, 
        string? code, 
        DiscountType type, 
        decimal value, 
        DiscountScope scope, 
        Guid? targetId,
        DateTime startDate,
        DateTime endDate,
        decimal? minimumOrderAmount = null,
        int? usageLimit = null)
    {
        return new Discount
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Code = code,
            Type = type,
            Value = value,
            Scope = scope,
            TargetId = targetId,
            StartDate = startDate,
            EndDate = endDate,
            MinimumOrderAmount = minimumOrderAmount,
            UsageLimit = usageLimit,
            IsActive = true
        };
    }
}
