using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Order
{
    /// <summary>
    /// DTO for updating an existing order item
    /// </summary>
    public class UpdateOrderItemsDTO
    {
        /// <summary>
        /// Unique identifier for the order item record
        /// </summary>
        public Guid OrderItems { get; init;}
            
        /// <summary>
        /// Unique identifier of the associated order
        /// </summary>
        public Guid OrderId { get; init; }

        /// <summary>
        /// Unique identifier of the product
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// Updated number of units
        /// </summary>
        public int Quantity { get; init; } = 1;

        /// <summary>
        /// Original creation timestamp
        /// </summary>
        public DateTime CreateAt { get; init; }
    }
}