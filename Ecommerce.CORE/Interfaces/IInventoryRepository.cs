using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    Task <Inventory> GetByProductIdAsync(Guid productId);
}
