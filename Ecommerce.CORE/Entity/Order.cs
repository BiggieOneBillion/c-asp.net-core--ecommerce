using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Order
{
   public OrderId OrderId { get; set; }

   public UserId UserId { get; set; }

   public PaymentId PaymentId { get; set; }

   
}
