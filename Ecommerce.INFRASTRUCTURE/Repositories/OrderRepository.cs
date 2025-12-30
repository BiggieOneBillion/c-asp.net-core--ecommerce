using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task CreateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
