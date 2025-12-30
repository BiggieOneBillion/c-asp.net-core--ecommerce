using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = Guid.NewGuid();
            var userId = UserId.Create(request.UserId);
            var paymentId = PaymentId.Create(request.PaymentId);

            var order = new Order(orderId, userId, paymentId);

            await _orderRepository.CreateAsync(order);

            return Result.Success(orderId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Order.CreateFailed", $"Failed to create order: {ex.Message}"));
        }
    }
}
