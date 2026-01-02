namespace Ecommerce.CORE.Common;

public abstract class AggregateRoot<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public TId Id { get; protected set; } = default!;
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
