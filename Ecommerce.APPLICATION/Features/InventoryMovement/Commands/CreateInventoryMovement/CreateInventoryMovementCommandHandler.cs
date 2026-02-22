using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Commands.CreateInventoryMovement;

public class CreateInventoryMovementCommandHandler : IRequestHandler<CreateInventoryMovementCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IInventoryMovementRepository _inventoryMovementRepository;

    public CreateInventoryMovementCommandHandler(IInventoryMovementRepository inventoryMovementRepository)
    {
        _inventoryMovementRepository = inventoryMovementRepository;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateInventoryMovementCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);

            var inventoryMovement = CORE.Entity.InventoryMovement.Create(
                productId:productId,
                quantityChanged:request.QuantityChanged,
                movementType:request.MovementType,
                reason:request.Reason!);

            await _inventoryMovementRepository.CreateAsync(inventoryMovement);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(inventoryMovement.Id.Id, "Inventory movement created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("InventoryMovement.CreateFailed", $"Failed to create inventory movement: {ex.Message}"));
        }
    }
}
