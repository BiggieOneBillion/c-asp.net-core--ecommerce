using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public DeleteOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        DeleteOrderItemCommand request,
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

            await _orderItemsRepository.DeleteAsync(orderItem);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Order item deleted successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("OrderItem.DeleteFailed", $"Failed to delete order item: {ex.Message}"));
        }
    }
}
