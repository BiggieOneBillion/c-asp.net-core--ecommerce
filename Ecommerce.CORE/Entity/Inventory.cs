using System;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.CORE.Enums;

namespace Ecommerce.CORE.Entity;

public class Inventory : AggregateRoot<InventoryId>
{
    public ProductId ProductId { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public int ReservedQuantity { get; private set; }

    // Private constructor for EF Core
    private Inventory() { }
    
    // Factory method
    public static Inventory Create(ProductId productId, int initialQuantity)
    {
        if (initialQuantity < 0)
            throw new DomainException("Initial stock quantity cannot be negative");
            
        return new Inventory
        {
            Id = InventoryId.Create(Guid.NewGuid()),
            ProductId = productId,
            StockQuantity = initialQuantity,
            ReservedQuantity = 0
        };
    }
    
    public void ReserveStock(int quantity, Guid orderId)
    {
        if (quantity <= 0)
            throw new DomainException("Reservation quantity must be greater than zero");
            
        if (StockQuantity < quantity)
            throw new DomainException("Insufficient stock to reserve");
            
        StockQuantity -= quantity;
        ReservedQuantity += quantity;
        
        RaiseDomainEvent(new InventoryReservedDomainEvent(
            Guid.Parse(ProductId.Value()), 
            quantity, 
            orderId
        ));
    }
    
    public void AdjustStock(int adjustment)
    {
        if (StockQuantity + adjustment < 0)
            throw new DomainException("Stock quantity cannot become negative");
            
        StockQuantity += adjustment;
    }
}
