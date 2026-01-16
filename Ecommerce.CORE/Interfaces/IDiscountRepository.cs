using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IDiscountRepository : IRepository<Discount>
{
    Task<Discount?> GetByCodeAsync(string code);
    Task<IEnumerable<Discount>> GetActiveDiscountsAsync();
}
