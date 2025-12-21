using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity
{
    public class InventoryMovement
    {
        public required InventoryMovementId InventoryMovementId { get; set; }

        public required ProductId ProductId { get; set; }

        public int QuantityChanged { get; set; }

        public InventoryMovementType MovementType { get; set; }

        public string? Reason { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public InventoryMovement(ProductId productId, int quantityChanged, InventoryMovementType movementType, string reason)
        {
            InventoryMovementId = InventoryMovementId.Create(Guid.NewGuid());
            ProductId = productId;
            QuantityChanged = quantityChanged;
            MovementType = movementType;
            Reason = reason;
            Timestamp = DateTime.UtcNow;
        }
    }
}