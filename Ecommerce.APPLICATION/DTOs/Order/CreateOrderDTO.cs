using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Order
{
    public record CreateOrderDTO
    {
        public Guid OrderId { get; init; }

        public Guid UserId { get; init; }

        public Guid PaymentId { get; init; } 
    }
}