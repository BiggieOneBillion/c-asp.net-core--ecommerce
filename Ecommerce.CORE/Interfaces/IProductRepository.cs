using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IProductRepository: IRepository<Product>
{
    Task<Product?> GetProductByNameAsync(string productName);

    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);

    Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
}
