using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IPaymentRepository: IRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId);
}
