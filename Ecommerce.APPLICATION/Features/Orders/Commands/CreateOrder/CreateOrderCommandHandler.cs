using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var paymentId = PaymentId.Create(request.PaymentId);

            var items = request.Items.Select(i => new OrderItems(
                OrderId.Create(Guid.Empty), // Will be set by factory
                ProductId.Create(i.ProductId),
                i.Quantity,
                i.PricePerUnit
            )).ToList();

            var order = Order.Create(userId, paymentId, items);

            await _unitOfWork.Orders.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(order.Id.Id);
        }
        catch (DomainException ex)
        {
            return Result.Failure<Guid>(
                new Error("Order.DomainError", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Order.CreateFailed", $"Failed to create order: {ex.Message}"));
        }
    }
}
