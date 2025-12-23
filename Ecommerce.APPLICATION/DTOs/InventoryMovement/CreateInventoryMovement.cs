using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.DTOs.InventoryMovement
{
    public record CreateInventoryMovement
    {
        public Guid InventoryMovementId { get; init; }

        public Guid ProductId { get; init; }

        public int QuantityChanged { get; init; }

        public InventoryMovementType MovementType { get; init; } = InventoryMovementType.StockIn;

        public string? Reason { get; init; }
    }
}