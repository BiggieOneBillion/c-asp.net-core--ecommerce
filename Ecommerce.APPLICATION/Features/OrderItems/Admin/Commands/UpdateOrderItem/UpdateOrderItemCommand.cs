using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.UpdateOrderItem;

public record UpdateOrderItemCommand(
    Guid OrderItemId,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    DateTime CreateAt
) : IRequest<Result<GeneralResponse<Unit>>>;
