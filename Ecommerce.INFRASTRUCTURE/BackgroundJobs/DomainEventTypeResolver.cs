using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;

namespace Ecommerce.INFRASTRUCTURE.BackgroundJobs
{
    public static class DomainEventTypeResolver
{
    private static readonly Dictionary<string, Type> Map = new() //! ADD TO THIS MAP WHEN YOU ADD NEW DOMAIN EVENTS AND MAKE SURE THE STRINGS MATCH THOSE IN DomainEventTypes
    {
        [DomainEventTypes.ProductCreated.ToLowerInvariant()] = typeof(ProductCreatedDomainEvent),
        [DomainEventTypes.UserCreated.ToLowerInvariant()] = typeof(UserCreatedDomainEvent),
        [DomainEventTypes.OrderCreated.ToLowerInvariant()] =  typeof(OrderCreatedDomainEvent),
        [DomainEventTypes.OrderConfirmed.ToLowerInvariant()] = typeof(OrderConfirmedDomainEvent),
        [DomainEventTypes.InventoryReserved.ToLowerInvariant()] = typeof(InventoryReservedDomainEvent),
        [DomainEventTypes.ProductPriceChanged.ToLowerInvariant()] = typeof(ProductPriceChangedDomainEvent)
    };

    public static Type Resolve(string eventType)
    {
        var normalized = eventType.ToLowerInvariant();

        if (!Map.TryGetValue(normalized, out var type))
        {
            throw new InvalidOperationException($"Type {eventType} not found");
        }

        return type;
    }
}
}