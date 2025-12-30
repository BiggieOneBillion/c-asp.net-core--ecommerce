using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Result>
{
    private readonly IOrderItemsRepository _orderItemsRepository;

    public DeleteOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task<Result> Handle(
        DeleteOrderItemCommand request,
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

            await _orderItemsRepository.DeleteAsync(orderItem);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("OrderItem.DeleteFailed", $"Failed to delete order item: {ex.Message}"));
        }
    }
}
