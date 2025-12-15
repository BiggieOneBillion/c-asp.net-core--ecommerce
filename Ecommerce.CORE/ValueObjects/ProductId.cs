using System;

namespace Ecommerce.CORE.Entity;

public class ProductId
{
   public Guid Id { get; set; }

     public string Value () => Id.ToString();

   public static ProductId Create(Guid id)
   {
      return new ProductId { Id = id };
   }
}
