using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.CreateOrderItem;

public record CreateOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    DateTime CreateAt
) : IRequest<Result<GeneralResponse<Guid>>>;
