using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

public record InventoryResponseDTO(
    Guid InventoryId,
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity,
    int AvailableQuantity);

public record InventoryMovementResponseDTO(
    Guid InventoryMovementId,
    Guid ProductId,
    int QuantityChanged,
    InventoryMovementType MovementType,
    string? Reason,
    DateTime Timestamp);

public record ProductPriceHistoryResponseDTO(
    Guid ProductPriceHistoryId,
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt);
