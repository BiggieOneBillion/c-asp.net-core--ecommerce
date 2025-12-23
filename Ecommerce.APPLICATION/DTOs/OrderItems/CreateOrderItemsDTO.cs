using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.OrderItems
{
    public record CreateOrderItemsDTO
    {

        public Guid OrderId { get; init; }

        public Guid ProductId { get; init; }

        public int Quantity { get; init; } = 1;

        public DateTime CreateAt { get; init; }
    }
}