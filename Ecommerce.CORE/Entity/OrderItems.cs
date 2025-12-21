using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class OrderItems
{
   public OrderItemsId OrderItemsId { get; set; }

   public OrderId OrderId { get; set; }

   public ProductId ProductId { get; set; }

   public int Quantity { get; set; } = 1;

   public decimal PricePerUnitAtPurchaseTime { get; set; }

   public DateTime CreateAt { get; set; }

    public OrderItems(OrderId orderId, ProductId productId, int quantity, decimal pricePerUnitAtPurchaseTime)
    {
        OrderItemsId = OrderItemsId.Create(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        PricePerUnitAtPurchaseTime = pricePerUnitAtPurchaseTime;
        CreateAt = DateTime.UtcNow;
    }


}
