using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class ProductRepository : IProductRepository
{
    public Task CreateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByNameAsync(string productName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }
}
