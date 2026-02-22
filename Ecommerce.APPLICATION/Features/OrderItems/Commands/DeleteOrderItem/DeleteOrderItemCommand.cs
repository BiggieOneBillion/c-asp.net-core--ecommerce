using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand(Guid OrderItemId) : IRequest<Result<GeneralResponse<Unit>>>;
