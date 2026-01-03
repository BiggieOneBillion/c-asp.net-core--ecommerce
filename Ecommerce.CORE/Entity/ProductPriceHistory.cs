using System;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class ProductPriceHistory : AggregateRoot<ProductPriceHistoryId>
{
    public ProductId ProductId { get; private set; } = null!;
    public decimal NewPrice { get; private set; }
    public decimal OldPrice { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime ChangedAt { get; private set; }

    // Private constructor for EF Core
    private ProductPriceHistory() { }

    public ProductPriceHistory(ProductId productId, decimal newPrice, decimal oldPrice, DateTime effectiveDate)
    {
        Id = ProductPriceHistoryId.Create(Guid.NewGuid());
        ProductId = productId;
        NewPrice = newPrice;
        OldPrice = oldPrice;
        EffectiveDate = effectiveDate < DateTime.UtcNow ? DateTime.UtcNow : effectiveDate;
        ChangedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(ProductId productId, decimal newPrice, decimal oldPrice, DateTime effectiveDate, DateTime changedAt)
    {
        ProductId = productId;
        NewPrice = newPrice;
        OldPrice = oldPrice;
        EffectiveDate = effectiveDate;
        ChangedAt = changedAt;
    }
}