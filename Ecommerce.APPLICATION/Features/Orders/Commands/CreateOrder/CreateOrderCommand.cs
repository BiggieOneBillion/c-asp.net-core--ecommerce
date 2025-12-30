using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    Guid PaymentId
) : ICommand<Guid>;
