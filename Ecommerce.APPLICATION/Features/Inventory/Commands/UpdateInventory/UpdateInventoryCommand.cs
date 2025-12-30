using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.UpdateInventory;

public record UpdateInventoryCommand(
    Guid InventoryId,
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity
) : ICommand;
