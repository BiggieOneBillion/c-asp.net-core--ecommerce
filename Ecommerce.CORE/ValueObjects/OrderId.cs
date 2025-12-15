using System;

namespace Ecommerce.CORE.ValueObjects;

public class OrderId
{
 public Guid Id { get; set; }

   public string Value () => Id.ToString();

   public static OrderId Create(Guid id)
   {
      return new OrderId { Id = id };
   }
}
