using System;

namespace Ecommerce.CORE.ValueObjects;

public class CategoryId
{
   public Guid Id { get; set; }

   public string Value () => Id.ToString();

   public static CategoryId Create(Guid id)
   {
      return new CategoryId { Id = id };
   }
}
