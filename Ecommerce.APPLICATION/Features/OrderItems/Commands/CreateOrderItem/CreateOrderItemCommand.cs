using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.CreateOrderItem;

public record CreateOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    DateTime CreateAt
) : ICommand<Guid>;
