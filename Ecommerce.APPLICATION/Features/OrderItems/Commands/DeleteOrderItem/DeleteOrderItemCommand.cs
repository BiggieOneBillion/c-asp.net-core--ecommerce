using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand(Guid OrderItemId) : ICommand;
