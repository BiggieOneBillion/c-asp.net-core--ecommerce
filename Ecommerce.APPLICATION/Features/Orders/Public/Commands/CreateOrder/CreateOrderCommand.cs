using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Public.Commands.CreateOrder;

/// <summary>
/// Command to create a new order
/// </summary>
public record CreateOrderCommand(
    Guid UserId,
    List<OrderItemDTO> Items,
    string? CouponCode = null
) : IRequest<Result<GeneralResponse<Guid>>>;

public record OrderItemDTO(
    Guid ProductId,
    int Quantity
);
