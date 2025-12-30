using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces
{
    public interface IProductPriceHistoryRepository: IRepository<ProductPriceHistory>
    {
        
        Task<IEnumerable<ProductPriceHistory>> GetByProductIdAsync(Guid productId);
    }
}