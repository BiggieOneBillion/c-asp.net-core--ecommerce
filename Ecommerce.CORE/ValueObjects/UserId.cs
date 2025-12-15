using System;

namespace Ecommerce.CORE.ValueObjects;

public class UserId
{
 public Guid Id { get; set; }

   public string Value () => Id.ToString();

   public static UserId Create(Guid id)
   {
      return new UserId { Id = id };
   }
}
