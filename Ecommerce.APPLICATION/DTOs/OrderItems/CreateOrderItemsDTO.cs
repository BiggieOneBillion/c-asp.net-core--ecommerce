using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.OrderItems
{
    /// <summary>
    /// DTO for creating an item entry within an order
    /// </summary>
    public record CreateOrderItemsDTO
    {
        /// <summary>
        /// Unique identifier of the associated order
        /// </summary>
        public Guid OrderId { get; init; }

        /// <summary>
        /// Unique identifier of the product being ordered
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// Number of units ordered (default: 1)
        /// </summary>
        public int Quantity { get; init; } = 1;

        /// <summary>
        /// Timestamp when the item was added to the order
        /// </summary>
        public DateTime CreateAt { get; init; }
    }
}