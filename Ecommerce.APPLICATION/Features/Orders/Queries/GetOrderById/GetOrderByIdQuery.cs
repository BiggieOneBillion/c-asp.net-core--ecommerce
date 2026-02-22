using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrderById;

/// <summary>
/// Query to retrieve an order by its unique identifier
/// </summary>
/// <param name="Id">Order ID</param>
public record GetOrderByIdQuery(Guid Id) : IRequest<Result<GeneralResponse<OrderResponseDTO>>>;
