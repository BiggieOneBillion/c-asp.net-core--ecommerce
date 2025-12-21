using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Order
{
   public OrderId OrderId { get; set; }

   public UserId UserId { get; set; }

   public PaymentId PaymentId { get; set; }

   public Order(Guid orderId, UserId userId, PaymentId paymentId)
   {
      OrderId = OrderId.Create(orderId);
      UserId = userId;
      PaymentId = paymentId;
   }
}
