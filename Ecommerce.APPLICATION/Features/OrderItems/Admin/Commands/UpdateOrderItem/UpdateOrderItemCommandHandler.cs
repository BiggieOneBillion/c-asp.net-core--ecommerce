using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public UpdateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderItemId = OrderItemsId.Create(request.OrderItemId);
            var orderItem = await _orderItemsRepository.GetByIdAsync(orderItemId.Id);

            if (orderItem == null)
            {
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("OrderItem.NotFound", $"Order item with ID {request.OrderItemId} not found"));
            }

            var orderId = OrderId.Create(request.OrderId);
            var productId = ProductId.Create(request.ProductId);

            // Call the Update method on the orderItem instance
            orderItem.Update( orderId, productId, request.Quantity, request.CreateAt);

            await _orderItemsRepository.UpdateAsync(orderItem);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Order item updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("OrderItem.UpdateFailed", $"Failed to update order item: {ex.Message}"));
        }
    }
}
