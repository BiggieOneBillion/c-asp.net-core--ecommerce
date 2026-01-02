using Ecommerce.CORE.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers;

public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly ILogger<OrderCreatedDomainEventHandler> _logger;

    public OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Domain Event Handled: {EventName}. Order {OrderId} created for User {UserId}. Total: {TotalAmount}",
            notification.GetType().Name,
            notification.OrderId,
            notification.UserId,
            notification.TotalAmount);

        // Side effect: E.g., Send email to customer, trigger inventory reservation background job, etc.
        
        return Task.CompletedTask;
    }
}
