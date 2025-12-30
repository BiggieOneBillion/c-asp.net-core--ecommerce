using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
}
