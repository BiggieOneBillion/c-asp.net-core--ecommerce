using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Order;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<CreateOrderDTO>;
