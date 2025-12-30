using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.CreateOrderItem;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Result<Guid>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public CreateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderItemId = Guid.NewGuid();
            var orderId = OrderId.Create(request.OrderId);
            var productId = ProductId.Create(request.ProductId);

            var orderItem = new OrderItems(
                orderItemId,
                orderId,
                productId,
                request.Quantity,
                request.CreateAt);

            await _orderItemsRepository.CreateAsync(orderItem);

            return Result.Success(orderItemId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("OrderItem.CreateFailed", $"Failed to create order item: {ex.Message}"));
        }
    }
}
