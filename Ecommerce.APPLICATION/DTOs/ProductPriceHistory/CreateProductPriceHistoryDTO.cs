using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.ProductPriceHistory
{
    public record CreateProductPriceHistoryDTO
    {

        public Guid ProductId { get; init; }

        public decimal NewPrice { get; init; }

        public decimal OldPrice { get; init; }

        public DateTime EffectiveDate { get; init; }

        public DateTime ChangedAt { get; init; } = DateTime.UtcNow;
    }
}