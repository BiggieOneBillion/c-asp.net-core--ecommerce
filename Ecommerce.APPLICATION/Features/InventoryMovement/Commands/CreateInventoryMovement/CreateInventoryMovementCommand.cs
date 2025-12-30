using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Commands.CreateInventoryMovement;

public record CreateInventoryMovementCommand(
    Guid ProductId,
    int QuantityChanged,
    InventoryMovementType MovementType,
    string? Reason = null
) : ICommand<Guid>;
