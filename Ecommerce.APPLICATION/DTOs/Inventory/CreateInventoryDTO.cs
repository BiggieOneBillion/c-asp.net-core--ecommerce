using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Inventory
{
    /// <summary>
    /// DTO for creating initial inventory for a product
    /// </summary>
    public record CreateInventoryDTO
    {
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// Initial number of units in stock (default: 0)
        /// </summary>
        public int StockQuantity { get; init; } = 0;

        /// <summary>
        /// Initial number of units reserved (default: 0)
        /// </summary>
        public int ReservedQuantity { get; init; } = 0;
    }
}