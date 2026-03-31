using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly Microsoft.Extensions.Logging.ILogger<ProductCreatedDomainEventHandler> _logger;

        public ProductCreatedDomainEventHandler(Microsoft.Extensions.Logging.ILogger<ProductCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event Handled: {EventName}. Product {ProductId} created.",
                notification.GetType().Name,
                notification.ProductId);

            // Inventory and InventoryMovement are now initialized synchronously in CreateProductCommandHandler.
            // This prevents duplicate key constraint violations in the Outbox processor job.
            
            return Task.CompletedTask;
        }
    }
}