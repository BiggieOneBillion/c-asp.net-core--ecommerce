using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
    }

    public async Task DeleteAsync(Order entity)
    {
        _context.Orders.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Create(id));
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == UserId.Create(userId))
            .ToListAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await Task.CompletedTask;
    }
}
