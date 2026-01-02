using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents;

public sealed class InventoryReservedDomainEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    public Guid OrderId { get; }
    
    public InventoryReservedDomainEvent(Guid productId, int quantity, Guid orderId)
    {
        ProductId = productId;
        Quantity = quantity;
        OrderId = orderId;
    }
}
