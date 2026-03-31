using System;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Payment : AggregateRoot<PaymentId>
{
    public PaymentType PaymentType { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public OrderId OrderId { get; private set; }

    // Private constructor for EF Core
    private Payment() { OrderId = default!; }

    // Factory method
    public static Payment Create(PaymentType paymentType, decimal amount, OrderId orderId)
    {
        if (amount <= 0)
            throw new DomainException("Payment amount must be greater than zero");

        return new Payment
        {
            Id = PaymentId.Create(Guid.NewGuid()),
            PaymentType = paymentType,
            Amount = amount,
            OrderId = orderId,
            PaymentDate = DateTime.UtcNow
        };
    }
}
