using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.OrderItems;

namespace Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemsByOrder;

public record GetOrderItemsByOrderQuery(
    Guid OrderId,
    int PageNumber = 1,
    int PageSize = 10
) : IQuery<PagedResult<CreateOrderItemsDTO>>;
