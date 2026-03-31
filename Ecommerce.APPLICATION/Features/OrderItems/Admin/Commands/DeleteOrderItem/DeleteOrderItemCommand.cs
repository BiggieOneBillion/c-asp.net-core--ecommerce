using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand(Guid OrderItemId) : IRequest<Result<GeneralResponse<Unit>>>;
