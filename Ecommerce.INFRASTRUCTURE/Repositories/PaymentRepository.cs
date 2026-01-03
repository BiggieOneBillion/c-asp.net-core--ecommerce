using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Payment entity)
    {
        await _context.Payments.AddAsync(entity);
    }

    public async Task DeleteAsync(Payment entity)
    {
        _context.Payments.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Payment?> GetByIdAsync(Guid id)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id.Id == id);
    }

    public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.Payments
            .Where(p => p.OrderId.Id == orderId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Payment entity)
    {
        _context.Payments.Update(entity);
        await Task.CompletedTask;
    }
}
