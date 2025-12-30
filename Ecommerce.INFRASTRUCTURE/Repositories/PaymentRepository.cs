using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task CreateAsync(Payment entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Payment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Payment entity)
    {
        throw new NotImplementedException();
    }
}
