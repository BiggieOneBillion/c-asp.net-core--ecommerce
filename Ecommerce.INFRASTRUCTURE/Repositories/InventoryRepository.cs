using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class InventoryRepository : IInventoryRepository
{
    public Task CreateAsync(Inventory entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Inventory entity)
    {
        throw new NotImplementedException();
    }

    public Task<Inventory?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Inventory entity)
    {
        throw new NotImplementedException();
    }
}
