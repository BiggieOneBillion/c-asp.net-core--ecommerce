using Ecommerce.CORE.Entity;

namespace Ecommerce.APPLICATION.Common.Interfaces;

public interface IDiscountService
{
    Task<decimal> CalculateDiscountAsync(Guid? userId, decimal orderTotal, List<(Guid ProductId, Guid CategoryId, decimal Price, int Quantity)> items, string? couponCode = null);
}
