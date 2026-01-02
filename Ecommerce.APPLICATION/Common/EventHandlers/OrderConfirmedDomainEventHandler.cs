using Ecommerce.CORE.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers;

public class OrderConfirmedDomainEventHandler : INotificationHandler<OrderConfirmedDomainEvent>
{
    private readonly ILogger<OrderConfirmedDomainEventHandler> _logger;

    public OrderConfirmedDomainEventHandler(ILogger<OrderConfirmedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Domain Event Handled: {EventName}. Order {OrderId} for User {UserId} is now confirmed.",
            notification.GetType().Name,
            notification.OrderId,
            notification.UserId);

        // Side effect: E.g., Notify shipping department, update statistics, etc.
        
        return Task.CompletedTask;
    }
}
