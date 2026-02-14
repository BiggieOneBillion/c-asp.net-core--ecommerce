using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Inventory
{
    /// <summary>
    /// DTO for updating inventory levels
    /// </summary>
    public record UpdateInventoryDTO
    {
        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid ProductId { get; init; }
        
        /// <summary>
        /// Unique identifier of the inventory record
        /// </summary>
        public Guid InventoryId { get; init; }

        /// <summary>
        /// Updated total stock quantity
        /// </summary>
        public int StockQuantity { get; init; } = 0;

        /// <summary>
        /// Updated reserved quantity
        /// </summary>
        public int ReservedQuantity { get; init; } = 0;
    }
}