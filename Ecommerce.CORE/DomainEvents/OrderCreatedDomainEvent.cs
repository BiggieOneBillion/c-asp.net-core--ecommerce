using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents;

public sealed class OrderCreatedDomainEvent : DomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public decimal TotalAmount { get; }
    public List<OrderItemData> Items { get; }
    
    public OrderCreatedDomainEvent(
        Guid orderId, 
        Guid userId, 
        decimal totalAmount, 
        List<OrderItemData> items)
    {
        OrderId = orderId;
        UserId = userId;
        TotalAmount = totalAmount;
        Items = items;
    }
}

public record OrderItemData(Guid ProductId, int Quantity, decimal PricePerUnit);
