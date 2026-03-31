using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Admin.Queries.GetInventoryMovementsByProduct;

public record GetInventoryMovementsByProductQuery(
    Guid ProductId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>>;
