using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.ProductPriceHistory
{
    /// <summary>
    /// DTO for creating a price history record
    /// </summary>
    public record CreateProductPriceHistoryDTO
    {
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// The new price being set
        /// </summary>
        public decimal NewPrice { get; init; }

        /// <summary>
        /// The previous price before the change
        /// </summary>
        public decimal OldPrice { get; init; }

        /// <summary>
        /// The date when the new price takes effect
        /// </summary>
        public DateTime EffectiveDate { get; init; }

        /// <summary>
        /// The timestamp when the record was created (default: UtcNow)
        /// </summary>
        public DateTime ChangedAt { get; init; } = DateTime.UtcNow;
    }
}