using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.CreateInventory;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<Guid>>
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateInventoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var inventoryId = Guid.NewGuid();
            var productId = ProductId.Create(request.ProductId);

            var inventory = new CORE.Entity.Inventory(
                productId: productId,
                inventoryId:inventoryId,
                request.StockQuantity,
                request.ReservedQuantity);

            await _inventoryRepository.CreateAsync(inventory);

            return Result.Success(inventoryId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Inventory.CreateFailed", $"Failed to create inventory: {ex.Message}"));
        }
    }
}
