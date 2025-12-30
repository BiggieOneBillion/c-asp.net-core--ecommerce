using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Queries.GetInventoryMovementsByProduct;

public record GetInventoryMovementsByProductQuery(
    Guid ProductId,
    int PageNumber = 1,
    int PageSize = 10
) : IQuery<PagedResult<InventoryMovementResponseDTO>>;
