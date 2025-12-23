using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Inventory
{
    public record CreateInventoryDTO
    {
        public Guid ProductId { get; init; }

        public int StockQuantity { get; init; } = 0;

        public int ReservedQuantity { get; init; } = 0;
    }
}