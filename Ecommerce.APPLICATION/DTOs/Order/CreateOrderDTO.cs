using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Order
{
    /// <summary>
    /// DTO representing the result of a created order
    /// </summary>
    public record CreateOrderDTO
    {
        /// <summary>
        /// Unique identifier for the created order
        /// </summary>
        public Guid OrderId { get; init; }

        /// <summary>
        /// Unique identifier for the user who placed the order
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Unique identifier for the payment associated with the order
        /// </summary>
        public Guid PaymentId { get; init; } 
    }
}