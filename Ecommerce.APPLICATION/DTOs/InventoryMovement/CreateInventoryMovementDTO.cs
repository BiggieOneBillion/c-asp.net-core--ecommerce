using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.DTOs.InventoryMovement
{
    public record CreateInventoryMovementDTO
    {
        public Guid ProductId { get; init; }
        public int QuantityChanged { get; init; }
        public InventoryMovementType MovementType { get; init; }
        public string? Reason { get; init; }
    }
}
