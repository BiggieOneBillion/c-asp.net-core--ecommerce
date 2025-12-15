using System;

namespace Ecommerce.CORE.ValueObjects;

public class PaymentId
{
   public Guid Id { get; set; }

     public string Value () => Id.ToString();

    public static PaymentId Create(Guid id)
    {
        return new PaymentId { Id = id };
    }
}
