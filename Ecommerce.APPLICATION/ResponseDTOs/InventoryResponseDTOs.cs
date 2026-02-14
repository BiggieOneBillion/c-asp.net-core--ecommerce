using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

/// <summary>
/// Response model for inventory details
/// </summary>
/// <param name="InventoryId">Unique identifier of the inventory record</param>
/// <param name="ProductId">Unique identifier of the associated product</param>
/// <param name="StockQuantity">Total units in stock</param>
/// <param name="ReservedQuantity">Units reserved for pending orders</param>
/// <param name="AvailableQuantity">Units available for new orders (Stock - Reserved)</param>
public record InventoryResponseDTO(
    Guid InventoryId,
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity,
    int AvailableQuantity);

/// <summary>
/// Response model for an inventory movement event
/// </summary>
/// <param name="InventoryMovementId">Unique identifier of the movement record</param>
/// <param name="ProductId">Unique identifier of the associated product</param>
/// <param name="QuantityChanged">The amount the stock changed (positive or negative)</param>
/// <param name="MovementType">The type of movement (e.g., Addition, Subtraction, Adjustment)</param>
/// <param name="Reason">Optional description or reason for the movement</param>
/// <param name="Timestamp">When the movement occurred</param>
public record InventoryMovementResponseDTO(
    Guid InventoryMovementId,
    Guid ProductId,
    int QuantityChanged,
    InventoryMovementType MovementType,
    string? Reason,
    DateTime Timestamp);

/// <summary>
/// Response model for product price history
/// </summary>
/// <param name="ProductPriceHistoryId">Unique identifier of the history record</param>
/// <param name="ProductId">Unique identifier of the associated product</param>
/// <param name="NewPrice">The price after the change</param>
/// <param name="OldPrice">The price before the change</param>
/// <param name="EffectiveDate">When the new price took effect</param>
/// <param name="ChangedAt">When the change was recorded</param>
public record ProductPriceHistoryResponseDTO(
    Guid ProductPriceHistoryId,
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt);
