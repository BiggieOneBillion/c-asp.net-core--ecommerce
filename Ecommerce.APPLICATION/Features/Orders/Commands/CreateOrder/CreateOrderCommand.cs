using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

/// <summary>
/// Command to create a new order
/// </summary>
/// <param name="UserId">Unique identifier of the user placing the order</param>
/// <param name="PaymentId">Unique identifier of the payment record</param>
/// <param name="Items">List of items to include in the order</param>
/// <param name="CouponCode">Optional coupon code for the order</param>
[HasPermission(Permissions.Orders.Create)]
public record CreateOrderCommand(
    Guid UserId,
    Guid PaymentId,
    List<CreateOrderItemCommand> Items,
    string? CouponCode = null
) : ICommand<Guid>;

/// <summary>
/// Command model for an individual order item
/// </summary>
/// <param name="ProductId">Unique identifier of the product</param>
/// <param name="Quantity">Number of units to purchase</param>
/// <param name="PricePerUnit">The price per unit at the time of order</param>
public record CreateOrderItemCommand(
    Guid ProductId,
    int Quantity,
    decimal PricePerUnit
);
