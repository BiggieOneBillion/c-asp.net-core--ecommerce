using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
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
        var paymentId = PaymentId.Create(id);
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId)
    {
        var ordId = OrderId.Create(orderId);
        return await _context.Payments
            .Where(p => p.OrderId == ordId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Payment entity)
    {
        _context.Payments.Update(entity);
        await Task.CompletedTask;
    }
}
