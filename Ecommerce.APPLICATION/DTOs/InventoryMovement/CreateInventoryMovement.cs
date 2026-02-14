using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.DTOs.InventoryMovement
{
    /// <summary>
    /// DTO for recording a stock movement event
    /// </summary>
    public record CreateInventoryMovement
    {
        /// <summary>
        /// Unique identifier for the movement record
        /// </summary>
        public Guid InventoryMovementId { get; init; }

        /// <summary>
        /// Unique identifier of the product affected
        /// </summary>
        public Guid ProductId { get; init; }

        /// <summary>
        /// The quantity by which the stock changed
        /// </summary>
        public int QuantityChanged { get; init; }

        /// <summary>
        /// The type of movement (e.g., StockIn, StockOut)
        /// </summary>
        public InventoryMovementType MovementType { get; init; } = InventoryMovementType.StockIn;

        /// <summary>
        /// Reason for the inventory adjustment
        /// </summary>
        public string? Reason { get; init; }
    }
}