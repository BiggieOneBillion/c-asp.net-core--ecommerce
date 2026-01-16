using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.APPLICATION.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountService(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<decimal> CalculateDiscountAsync(
        Guid? userId, 
        decimal orderTotal, 
        List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)> items, 
        string? couponCode = null)
    {
        var activeDiscounts = (await _discountRepository.GetActiveDiscountsAsync()).ToList();
        decimal totalDiscount = 0;

        // 1. Handle Coupon Code (Global or specific)
        if (!string.IsNullOrEmpty(couponCode))
        {
            var coupon = await _discountRepository.GetByCodeAsync(couponCode);
            if (coupon != null && IsEligible(coupon, userId, orderTotal))
            {
                totalDiscount += ApplyDiscount(coupon, orderTotal, items);
                // If a coupon is applied, we might decide it's exclusive or not. 
                // For now, let's assume it can stack with automatic product/category discounts.
            }
        }

        // 2. Handle Automatic Discounts (Product, Category, Global)
        var automaticDiscounts = activeDiscounts.Where(d => string.IsNullOrEmpty(d.Code)).ToList();

        foreach (var discount in automaticDiscounts)
        {
            if (IsEligible(discount, userId, orderTotal))
            {
                totalDiscount += ApplyDiscount(discount, orderTotal, items);
            }
        }

        return totalDiscount;
    }

    private bool IsEligible(Discount discount, Guid? userId, decimal orderTotal)
    {
        if (discount.MinimumOrderAmount.HasValue && orderTotal < discount.MinimumOrderAmount.Value)
            return false;

        if (discount.UsageLimit.HasValue && discount.UsageCount >= discount.UsageLimit.Value)
            return false;

        if (discount.Scope == DiscountScope.Customer && discount.TargetId != userId)
            return false;

        return true;
    }

    private decimal ApplyDiscount(Discount discount, decimal orderTotal, List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)> items)
    {
        switch (discount.Scope)
        {
            case DiscountScope.Global:
                return CalculateValue(discount, orderTotal);

            case DiscountScope.Product:
                var productItems = items.Where(i => i.ProductId == discount.TargetId);
                decimal productTotal = productItems.Sum(i => i.Price * i.Quantity);
                return CalculateValue(discount, productTotal);

            case DiscountScope.Category:
                var categoryItems = items.Where(i => i.CategoryId == discount.TargetId);
                decimal categoryTotal = categoryItems.Sum(i => i.Price * i.Quantity);
                return CalculateValue(discount, categoryTotal);

            case DiscountScope.Customer:
                return CalculateValue(discount, orderTotal);

            default:
                return 0;
        }
    }

    private decimal CalculateValue(Discount discount, decimal amount)
    {
        if (discount.Type == DiscountType.Percentage)
        {
            return amount * (discount.Value / 100);
        }
        else // FixedAmount
        {
            return Math.Min(discount.Value, amount); // Cannot discount more than the amount
        }
    }
}
