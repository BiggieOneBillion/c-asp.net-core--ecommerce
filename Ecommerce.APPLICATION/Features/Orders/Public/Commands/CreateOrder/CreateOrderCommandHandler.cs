using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Public.Commands.CreateOrder;

public class CreateOrderCommandHandler 
    : IRequestHandler<CreateOrderCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IDiscountService _discountService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IInventoryRepository inventoryRepository,
        IDiscountService discountService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        _discountService = discountService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var orderItemsList = new List<Ecommerce.CORE.Entity.OrderItems>();
            var discountInputItems = new List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)>();
            decimal subtotal = 0;

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    return Result.Failure<GeneralResponse<Guid>>(
                        new Error("Order.ProductNotFound", $"Product with ID {item.ProductId} not found"));

                var inventory = await _inventoryRepository.GetByProductIdAsync(product.Id.Id);
                if (inventory == null || inventory.StockQuantity < item.Quantity)
                    return Result.Failure<GeneralResponse<Guid>>(
                        new Error("Order.InsufficientStock", $"Insufficient stock for product {product.Name}"));

                // We'll create order items.
                // The current OrderItems entity constructor requires an OrderId which is unknown at this point.
                // However, the factory method Order.Create will handle the collection.
                // We'll pass null for orderId and let the factory or EF Core handle it if possible, 
                // but OrderItems constructor expects it. 
                // Let's use Guid.Empty as a placeholder if necessary, but ideally we'd have a way to create them without OrderId.
                
                var orderItem = new Ecommerce.CORE.Entity.OrderItems(
                    OrderId.Create(Guid.Empty), // Placeholder
                    product.Id,
                    item.Quantity,
                    product.CurrentPrice
                );

                orderItemsList.Add(orderItem);
                subtotal += product.CurrentPrice * item.Quantity;
                
                discountInputItems.Add((product.Id.Id, product.CategoryId.Id, product.CurrentPrice, item.Quantity));
                
                // Deduct stock
                inventory.AdjustStock(-item.Quantity);
                await _inventoryRepository.UpdateAsync(inventory);
            }

            // Calculate discounts
            var (discountAmount, appliedDiscountId) = await _discountService.CalculateDiscountAsync(
                request.UserId, 
                subtotal, 
                discountInputItems, 
                request.CouponCode);

            // Create Order using factory
            var tempPaymentId = PaymentId.Create(Guid.Empty); 

            var order = Order.Create(
                userId,
                tempPaymentId,
                orderItemsList,
                discountAmount,
                appliedDiscountId
            );

            await _orderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(order.Id.Id, "Order created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("Order.CreateFailed", $"Failed to create order: {ex.Message}"));
        }
    }
}
