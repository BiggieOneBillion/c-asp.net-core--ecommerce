using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.CreateInventory;

public record CreateInventoryCommand(
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity = 0
) : ICommand<Guid>;
