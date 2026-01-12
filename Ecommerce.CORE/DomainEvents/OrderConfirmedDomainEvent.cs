using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents;

public sealed class OrderConfirmedDomainEvent : DomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }
    
    public OrderConfirmedDomainEvent(Guid orderId, Guid userId)
    {
        OrderId = orderId;
        UserId = userId;
    }

     public override string EventType()
    {
        return DomainEventTypes.OrderConfirmed;
    }
}
