using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories
{
    public class ProductPriceHistoryRepository : IProductPriceHistoryRepository
    {
        public Task CreateAsync(ProductPriceHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ProductPriceHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductPriceHistory?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductPriceHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}