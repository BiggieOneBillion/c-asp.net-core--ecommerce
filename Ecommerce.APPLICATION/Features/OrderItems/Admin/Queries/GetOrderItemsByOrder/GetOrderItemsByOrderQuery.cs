using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Queries.GetOrderItemsByOrder;

public record GetOrderItemsByOrderQuery(
    Guid OrderId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<GeneralResponse<PagedResult<OrderItemResponseDTO>>>>;
