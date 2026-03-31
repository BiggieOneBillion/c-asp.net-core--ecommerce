using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Admin.Commands.UpdateInventory;

[HasPermission(Permissions.Inventory.Manage)]
public record UpdateInventoryCommand(
    Guid InventoryId,
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity
) : IRequest<Result<GeneralResponse<Unit>>>;
