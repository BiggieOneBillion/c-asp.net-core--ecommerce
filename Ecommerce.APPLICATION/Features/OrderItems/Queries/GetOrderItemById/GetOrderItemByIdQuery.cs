using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.OrderItems;

namespace Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemById;

public record GetOrderItemByIdQuery(Guid OrderItemId) : IQuery<CreateOrderItemsDTO>;
