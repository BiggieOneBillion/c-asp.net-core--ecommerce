
using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents
{
    public class ProductCreatedDomainEvent: DomainEvent
    {
        public Guid ProductId { get; }
        public int StockQuantity { get; }

         public override string EventType()
    {
        return DomainEventTypes.ProductCreated;
    }

        public ProductCreatedDomainEvent(Guid productId, int stockQuantity)
        {
            ProductId = productId;
            StockQuantity = stockQuantity;
        }
        
    }
}