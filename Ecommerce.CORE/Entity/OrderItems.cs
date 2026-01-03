using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class OrderItems
{
    public OrderItemsId Id { get; private set; } = null!;
    public OrderId OrderId { get; private set; } = null!;
    public ProductId ProductId { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal PricePerUnitAtPurchaseTime { get; private set; }
    public DateTime CreateAt { get; private set; }

    // Private constructor for EF Core
    private OrderItems() { }

    public OrderItems(OrderId orderId, ProductId productId, int quantity, decimal pricePerUnitAtPurchaseTime)
    {
        Id = OrderItemsId.Create(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        PricePerUnitAtPurchaseTime = pricePerUnitAtPurchaseTime;
        CreateAt = DateTime.UtcNow;
    }

    public void Update( 
        OrderId orderId,
        ProductId productId,
        int quantity,
        DateTime createAt)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        CreateAt = createAt;
    }   
}
