using System;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.CORE.Enums;

namespace Ecommerce.CORE.Entity;

public class Inventory
{
   public ProductId ProductId { get; set; }

   public InventoryId InventoryId { get; set; }

   public int StockQuantity { get; set; } = 0;

    public int ReservedQuantity { get; set; } = 0;

   public int AvaliableQuantity () => StockQuantity - ReservedQuantity;

   public InventoryType  InventoryType { get; set; } = InventoryType.Stock;

}
