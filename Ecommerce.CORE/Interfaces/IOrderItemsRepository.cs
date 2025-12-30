using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IOrderItemsRepository : IRepository<OrderItems>
{
    Task<IEnumerable<OrderItems>> GetByOrderIdAsync(Guid orderId);
}
