using System;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class InventoryMovement : AggregateRoot<InventoryMovementId>
{
    public ProductId ProductId { get; private set; } = null!;
    public int QuantityChanged { get; private set; }
    public InventoryMovementType MovementType { get; private set; }
    public string? Reason { get; private set; }
    public DateTime Timestamp { get; private set; }

    // Private constructor for EF Core
    private InventoryMovement() { }

    // Factory method
    public static InventoryMovement Create(ProductId productId, int quantityChanged, InventoryMovementType movementType, string reason)
    {
        return new InventoryMovement
        {
            Id = InventoryMovementId.Create(Guid.NewGuid()),
            ProductId = productId,
            QuantityChanged = quantityChanged,
            MovementType = movementType,
            Reason = reason,
            Timestamp = DateTime.UtcNow
        };
    }
}