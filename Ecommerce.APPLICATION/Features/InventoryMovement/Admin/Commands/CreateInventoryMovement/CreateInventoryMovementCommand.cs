using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Enums;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Admin.Commands.CreateInventoryMovement;

public record CreateInventoryMovementCommand(
    Guid ProductId,
    int QuantityChanged,
    InventoryMovementType MovementType,
    string? Reason = null
) : IRequest<Result<GeneralResponse<Guid>>>;
