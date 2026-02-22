using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrdersByUser;

/// <summary>
/// Query to retrieve all orders for a specific user
/// </summary>
/// <param name="UserId">User ID</param>
public record GetOrdersByUserQuery(Guid UserId) : IRequest<Result<GeneralResponse<List<OrderResponseDTO>>>>;
