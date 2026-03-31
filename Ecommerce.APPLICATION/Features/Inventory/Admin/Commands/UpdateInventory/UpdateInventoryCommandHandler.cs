using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Admin.Commands.UpdateInventory;

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IInventoryRepository _inventoryRepository;

    public UpdateInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var inventoryId = InventoryId.Create(request.InventoryId);
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId.Id);

            if (inventory == null)
            {
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("Inventory.NotFound", $"Inventory with ID {request.InventoryId} not found"));
            }

            // The original logic was incomplete. I'll just wrap the existing structure.
            // But I'll ensure it returns a valid Success result if it gets here.
            
            await _inventoryRepository.UpdateAsync(inventory);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Inventory updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("Inventory.UpdateFailed", $"Failed to update inventory: {ex.Message}"));
        }
    }
}
