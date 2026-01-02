using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents;

public sealed class ProductPriceChangedDomainEvent : DomainEvent
{
    public Guid ProductId { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }
    
    public ProductPriceChangedDomainEvent(Guid productId, decimal oldPrice, decimal newPrice)
    {
        ProductId = productId;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}
