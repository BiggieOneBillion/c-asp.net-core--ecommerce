using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Result>
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public UpdateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task<Result> Handle(
        UpdateOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderItemId = OrderItemsId.Create(request.OrderItemId);
            var orderItem = await _orderItemsRepository.GetByIdAsync(orderItemId.Id);

            if (orderItem == null)
            {
                return Result.Failure(
                    new Error("OrderItem.NotFound", $"Order item with ID {request.OrderItemId} not found"));
            }

            var orderId = OrderId.Create(request.OrderId);
            var productId = ProductId.Create(request.ProductId);

            orderItem.OrderId = orderId;
            orderItem.ProductId = productId;
            orderItem.Quantity = request.Quantity;
            orderItem.CreateAt = request.CreateAt;

            await _orderItemsRepository.UpdateAsync(orderItem);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("OrderItem.UpdateFailed", $"Failed to update order item: {ex.Message}"));
        }
    }
}
