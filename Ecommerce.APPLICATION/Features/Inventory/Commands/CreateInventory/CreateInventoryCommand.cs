using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Commands.CreateInventory;

public record CreateInventoryCommand(
    Guid ProductId,
    int StockQuantity,
    int ReservedQuantity = 0
) : IRequest<Result<GeneralResponse<Guid>>>;
