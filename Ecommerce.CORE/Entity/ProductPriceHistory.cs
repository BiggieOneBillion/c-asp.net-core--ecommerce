using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity
{
    public class ProductPriceHistory
    {
        public required ProductPriceHistoryId ProductPriceHistoryId { get; set; }

        public required ProductId ProductId { get; set; }

        public decimal NewPrice { get; set; }

        public decimal OldPrice { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public ProductPriceHistory(ProductId productId, decimal newPrice, decimal oldPrice, DateTime effectiveDate)
        {
            ProductPriceHistoryId = ProductPriceHistoryId.Create(Guid.NewGuid());
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
            if (effectiveDate < DateTime.UtcNow)
            {
                EffectiveDate = DateTime.UtcNow;
            } else
            {
                EffectiveDate = effectiveDate;
            }
        }
    }
}