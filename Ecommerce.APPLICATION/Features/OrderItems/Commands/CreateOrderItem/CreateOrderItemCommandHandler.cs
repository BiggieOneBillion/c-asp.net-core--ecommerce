using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Commands.CreateOrderItem;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository, IProductRepository productRepository)
    {
        _orderItemsRepository = orderItemsRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateOrderItemCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var productId = ProductId.Create(request.ProductId);

            // check for product and get the price at purchase time if needed
            var product = await _productRepository.GetByIdAsync(productId.Id);

            if(product == null)
            {
                return Result.Failure<GeneralResponse<Guid>>(
                    new Error("Product.NotFound", $"Product with ID {request.ProductId} not found"));
            }


            CORE.Entity.OrderItems orderItem = new CORE.Entity.OrderItems(
                orderId:orderId,
                productId:productId,
                quantity:request.Quantity,
                pricePerUnitAtPurchaseTime: product!.CurrentPrice
                );

            await _orderItemsRepository.CreateAsync(orderItem);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(orderItem.Id.Id, "Order item created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("OrderItem.CreateFailed", $"Failed to create order item: {ex.Message}"));
        }
    }
}
