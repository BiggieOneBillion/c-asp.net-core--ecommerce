using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IProductRepository: IRepository<Product>
{
    Task<Product?> GetProductByNameAsync(string productName);

}
