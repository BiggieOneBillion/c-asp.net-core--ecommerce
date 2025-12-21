using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class OrderItemsRepository : IOrderItemsRepository
{
    public Task AddAsync(OrderItems entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderItems entity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItems?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderItems entity)
    {
        throw new NotImplementedException();
    }
}
