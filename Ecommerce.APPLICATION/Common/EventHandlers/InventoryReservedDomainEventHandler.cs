using Ecommerce.CORE.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers;

public class InventoryReservedDomainEventHandler : INotificationHandler<InventoryReservedDomainEvent>
{
    private readonly ILogger<InventoryReservedDomainEventHandler> _logger;

    public InventoryReservedDomainEventHandler(ILogger<InventoryReservedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(InventoryReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Domain Event Handled: {EventName}. Product {ProductId}: {Quantity} items reserved for Order {OrderId}.",
            notification.GetType().Name,
            notification.ProductId,
            notification.Quantity,
            notification.OrderId);

        // Side effect: Post-reservation logic if any.
        
        return Task.CompletedTask;
    }
}
