using System;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Payment
{
   public required PaymentId PaymentId { get; set; }

   public PaymentType PaymentType { get; set;}

   public required decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public required OrderId OrderId { get; set; }

    public Payment(PaymentType paymentType, decimal amount, OrderId orderId)
    {
        PaymentId = PaymentId.Create(Guid.NewGuid());
        PaymentType = paymentType;
        Amount = amount;
        OrderId = orderId;
        PaymentDate = DateTime.UtcNow;
    }
}
