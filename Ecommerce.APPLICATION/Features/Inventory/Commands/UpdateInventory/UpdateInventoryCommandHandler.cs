using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
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

    public async Task<Result> Handle( //! NOT COMPLETE -- PAY ATTENTION.
        UpdateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var inventoryId = InventoryId.Create(request.InventoryId);
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryId.Id);

            if (inventory == null)
            {
                return Result.Failure(
                    new Error("Inventory.NotFound", $"Inventory with ID {request.InventoryId} not found"));
            }

            var productId = ProductId.Create(request.ProductId);

            if (request.StockQuantity < 0)
            {
                return Result.Failure(
                    new Error("Inventory.InvalidStockQuantity", "Stock quantity cannot be negative"));
            }

            if (request.ReservedQuantity < 0)
            {
                return Result.Failure(
                    new Error("Inventory.InvalidReservedQuantity", "Reserved quantity cannot be negative"));
            }

            // if (request.StockQuantity > 0 && request.ReservedQuantity > 0)
            // {
            //     inventory.AdjustStock(request.StockQuantity - inventory.StockQuantity);
            //     // Note: ReservedQuantity adjustment logic can be added here if needed
            //     inventory.ReserveStock();
            // }

            // inventory.ProductId = productId;
            // inventory.StockQuantity = request.StockQuantity;
            // inventory.ReservedQuantity = request.ReservedQuantity;

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
