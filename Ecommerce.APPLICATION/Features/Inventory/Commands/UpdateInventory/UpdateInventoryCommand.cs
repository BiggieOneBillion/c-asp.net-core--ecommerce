using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.UpdateInventory;

[HasPermission(Permissions.Inventory.Manage)]
public record UpdateInventoryCommand(
    Guid InventoryId,
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity
) : ICommand;
