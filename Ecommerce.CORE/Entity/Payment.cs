using System;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Payment
{
   public PaymentId PaymentId { get; set; }

   public PaymentType PaymentType { get; set;}

   public required decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    
}
