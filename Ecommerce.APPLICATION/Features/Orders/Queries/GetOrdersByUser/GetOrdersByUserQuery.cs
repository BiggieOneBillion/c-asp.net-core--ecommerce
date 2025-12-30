using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrdersByUser;

public record GetOrdersByUserQuery(
    Guid UserId,
    int PageNumber = 1,
    int PageSize = 10
) : IQuery<PagedResult<OrderResponseDTO>>;
