using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IDiscountService _discountService;

    public CreateOrderCommandHandler(
        IUnitOfWork unitOfWork, 
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IDiscountRepository discountRepository,
        IDiscountService discountService)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _discountService = discountService;
    }

    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var paymentId = PaymentId.Create(request.PaymentId);

            // Fetch products to get CategoryIds for discount calculation
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            var products = await _productRepository.GetAllAsync(); // In a real app, use GetByIdsAsync
            var productsMap = products.Where(p => productIds.Contains(p.Id.Id))
                                      .ToDictionary(p => p.Id.Id, p => p.CategoryId.Id);

            var discountItems = request.Items.Select(i => (
                i.ProductId,
                productsMap.GetValueOrDefault(i.ProductId),
                i.PricePerUnit,
                i.Quantity
            )).ToList();

            decimal subTotal = request.Items.Sum(i => i.PricePerUnit * i.Quantity);
            var (discountAmount, appliedDiscountId) = await _discountService.CalculateDiscountAsync(
                request.UserId, 
                subTotal, 
                discountItems, 
                request.CouponCode);

            // Increment UsageCount if a discount was applied
            if (appliedDiscountId.HasValue)
            {
                var discount = await _discountRepository.GetByIdAsync(appliedDiscountId.Value);
                if (discount != null)
                {
                    discount.UsageCount++;
                    await _discountRepository.UpdateAsync(discount);
                }
            }

            var items = request.Items.Select(i => new CORE.Entity.OrderItems(
                OrderId.Create(Guid.Empty), // Will be set by factory
                ProductId.Create(i.ProductId),
                i.Quantity,
                i.PricePerUnit
            )).ToList();

            var order = Order.Create(userId, paymentId, items, discountAmount, appliedDiscountId);

            await _orderRepository.CreateAsync(order);
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
