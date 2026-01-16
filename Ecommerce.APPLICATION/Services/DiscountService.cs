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

    public async Task<(decimal DiscountAmount, Guid? AppliedDiscountId)> CalculateDiscountAsync(
        Guid? userId, 
        decimal orderTotal, 
        List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)> items, 
        string? couponCode = null)
    {
        var activeDiscounts = (await _discountRepository.GetActiveDiscountsAsync()).ToList();
        decimal totalDiscount = 0;
        Guid? appliedDiscountId = null;

        // 1. Handle Coupon Code (Global or specific) - Coupons usually take priority or are specific
        if (!string.IsNullOrEmpty(couponCode))
        {
            var coupon = await _discountRepository.GetByCodeAsync(couponCode);
            if (coupon != null && IsEligible(coupon, userId, orderTotal))
            {
                totalDiscount += ApplyDiscount(coupon, orderTotal, items);
                appliedDiscountId = coupon.Id;
                // If a coupon is applied, we'll return it as the primary applied discount
                return (totalDiscount, appliedDiscountId);
            }
        }

        // 2. Handle Automatic Discounts (Product, Category, Global)
        var automaticDiscounts = activeDiscounts.Where(d => string.IsNullOrEmpty(d.Code)).ToList();

        foreach (var discount in automaticDiscounts)
        {
            if (IsEligible(discount, userId, orderTotal))
            {
                var discountValue = ApplyDiscount(discount, orderTotal, items);
                if (discountValue > 0)
                {
                    totalDiscount += discountValue;
                    // For automatic ones, we might track the last one or the one with most value. 
                    // Let's track the first one that provided value for simplicity in analytics for now.
                    appliedDiscountId ??= discount.Id;
                }
            }
        }

        return (totalDiscount, appliedDiscountId);
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
