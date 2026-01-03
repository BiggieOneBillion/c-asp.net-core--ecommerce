using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers;

public class ProductPriceChangedDomainEventHandler : INotificationHandler<ProductPriceChangedDomainEvent>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;
    private readonly ILogger<ProductPriceChangedDomainEventHandler> _logger;

    public ProductPriceChangedDomainEventHandler(
        IProductPriceHistoryRepository priceHistoryRepository,
        ILogger<ProductPriceChangedDomainEventHandler> logger)
    {
        _priceHistoryRepository = priceHistoryRepository;
        _logger = logger;
    }

    public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Handling ProductPriceChangedDomainEvent for Product {ProductId}. Price changed from {OldPrice} to {NewPrice}",
            notification.ProductId,
            notification.OldPrice,
            notification.NewPrice);

        var historyRecord = new ProductPriceHistory(
            ProductId.Create(notification.ProductId),
            notification.NewPrice,
            notification.OldPrice,
            DateTime.UtcNow
        );

        await _priceHistoryRepository.CreateAsync(historyRecord);
        
        // Note: The PriceHistory record will be persisted by the outbox processor 
        // when it calls SaveChangesAsync after publishing the event, 
        // OR if this handler is invoked in-process (not common for outbox, but possible).
        // Since the current Outbox processor (ProcessOutboxMessagesJob) calls SaveChangesAsync 
        // at the end of the batch, the history will be saved correctly.
    }
}
