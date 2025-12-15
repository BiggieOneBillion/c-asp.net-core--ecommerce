using System;

namespace Ecommerce.CORE.ValueObjects;

public class InventoryId
{ 
   public Guid Id { get; set; }

     public string Value () => Id.ToString();

   public static InventoryId Create(Guid id)
   {
      return new InventoryId { Id = id };
   }
}
