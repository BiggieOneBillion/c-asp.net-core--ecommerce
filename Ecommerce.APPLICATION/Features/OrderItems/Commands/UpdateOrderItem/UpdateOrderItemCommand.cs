using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.UpdateOrderItem;

public record UpdateOrderItemCommand(
    Guid OrderItemId,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    DateTime CreateAt
) : ICommand;
