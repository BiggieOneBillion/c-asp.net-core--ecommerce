using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Order
{
    public class UpdateOrderItemsDTO
    {
        public Guid OrderItems { get; init;}
            
        public Guid OrderId { get; init; }

        public Guid ProductId { get; init; }

        public int Quantity { get; init; } = 1;

        public DateTime CreateAt { get; init; }
    }
}