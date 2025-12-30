using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.UpdateInventory;

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Result>
{
    private readonly IInventoryRepository _inventoryRepository;

    public UpdateInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result> Handle(
        UpdateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var inventoryId = InventoryId.Create(request.InventoryId);
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId);

            if (inventory == null)
            {
                return Result.Failure(
                    new Error("Inventory.NotFound", $"Inventory with ID {request.InventoryId} not found"));
            }

            var productId = ProductId.Create(request.ProductId);

            inventory.ProductId = productId;
            inventory.StockQuantity = request.StockQuantity;
            inventory.ReservedQuantity = request.ReservedQuantity;

            await _inventoryRepository.UpdateAsync(inventory);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("Inventory.UpdateFailed", $"Failed to update inventory: {ex.Message}"));
        }
    }
}
