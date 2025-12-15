using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class OrderItems
{
   public OrderItemsId OrderItemsId { get; set; }

   public OrderId OrderId { get; set; }

   public ProductId ProductId { get; set; }

   public int Quantity { get; set; } = 1;

  public DateTime CreateAt { get; set; }


}
