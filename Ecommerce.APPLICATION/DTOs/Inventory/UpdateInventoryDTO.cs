using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Inventory
{
    public record UpdateInventoryDTO
    {
        public Guid ProductId { get; init; }
        
        public Guid InventoryId { get; init; }

        public int StockQuantity { get; init; } = 0;

        public int ReservedQuantity { get; init; } = 0;
    }
}