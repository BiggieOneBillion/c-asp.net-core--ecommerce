using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Commands.CreateInventoryMovement;

public class CreateInventoryMovementCommandHandler : IRequestHandler<CreateInventoryMovementCommand, Result<Guid>>
{
    private readonly IInventoryMovementRepository _inventoryMovementRepository;

    public CreateInventoryMovementCommandHandler(IInventoryMovementRepository inventoryMovementRepository)
    {
        _inventoryMovementRepository = inventoryMovementRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateInventoryMovementCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var movementId = Guid.NewGuid();
            var productId = ProductId.Create(request.ProductId);

            var inventoryMovement = new CORE.Entity.InventoryMovement(
                movementId,
                productId,
                request.QuantityChanged,
                request.MovementType,
                request.Reason);

            await _inventoryMovementRepository.CreateAsync(inventoryMovement);

            return Result.Success(movementId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("InventoryMovement.CreateFailed", $"Failed to create inventory movement: {ex.Message}"));
        }
    }
}
