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

namespace Ecommerce.APPLICATION.Common.EventHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {

        private readonly IInventoryRepository inventoryRepository;
        private readonly IInventoryMovementRepository inventoryMovementLogRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductCreatedDomainEventHandler(
            IInventoryRepository inventoryRepository,
            IInventoryMovementRepository inventoryMovementLogRepository,
            IUnitOfWork unitOfWork)
        {
            this.inventoryRepository = inventoryRepository;
            this.inventoryMovementLogRepository = inventoryMovementLogRepository;
             _unitOfWork = unitOfWork;
        }
        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {

                var inventory = Inventory.Create(
                ProductId.Create(notification.ProductId),
                notification.StockQuantity);

                await inventoryRepository.CreateAsync(inventory);

                var inventoryMovement = InventoryMovement.Create(
                    ProductId.Create(notification.ProductId),
                    notification.StockQuantity,
                    0,
                    "Initial stock for new product");

                await inventoryMovementLogRepository.CreateAsync(inventoryMovement);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}