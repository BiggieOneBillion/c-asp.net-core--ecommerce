using MediatR;

namespace Ecommerce.CORE.Common;

public abstract class DomainEvent : IDomainEvent, INotification
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public abstract string EventType();
}
