using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

[HasPermission(Permissions.Orders.Create)]
public record CreateOrderCommand(
    Guid UserId,
    Guid PaymentId,
    List<CreateOrderItemCommand> Items
) : ICommand<Guid>;

public record CreateOrderItemCommand(
    Guid ProductId,
    int Quantity,
    decimal PricePerUnit
);
