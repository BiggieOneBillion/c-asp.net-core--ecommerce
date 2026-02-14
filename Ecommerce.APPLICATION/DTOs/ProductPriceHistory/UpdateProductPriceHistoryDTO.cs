using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.ProductPriceHistory
{
    /// <summary>
    /// DTO for updating an existing price history record
    /// </summary>
    public record UpdateProductPriceHistoryDTO
    {
        /// <summary>
        /// Unique identifier for the price history record
        /// </summary>
        public Guid ProductPriceHistoryId { get; init; }
        
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// Updated new price
        /// </summary>
        public decimal NewPrice { get; init; }

        /// <summary>
        /// Updated old price
        /// </summary>
        public decimal OldPrice { get; init; }

        /// <summary>
        /// Updated effective date
        /// </summary>
        public DateTime EffectiveDate { get; init; }

        /// <summary>
        /// Updated change timestamp
        /// </summary>
        public DateTime ChangedAt { get; init; } = DateTime.UtcNow;
    }
}