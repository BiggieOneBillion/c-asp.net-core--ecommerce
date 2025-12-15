using System;

namespace Ecommerce.CORE.ValueObjects;

public class OrderItemsId
{
 public Guid Id { get; set; }

   public string Value () => Id.ToString();

   public static OrderItemsId Create(Guid id)
   {
      return new OrderItemsId { Id = id };
   }
}
