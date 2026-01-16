using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class OrderItemsRepository : IOrderItemsRepository
{
    private readonly ApplicationDbContext _context;

    public OrderItemsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(OrderItems entity)
    {
        await _context.OrderItems.AddAsync(entity);
    }

    public async Task DeleteAsync(OrderItems entity)
    {
        _context.OrderItems.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<OrderItems?> GetByIdAsync(Guid id)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(o => o.Id.Id == id);
    }

    public async Task<IEnumerable<OrderItems>> GetAllAsync()
    {
        return await _context.OrderItems.ToListAsync();
    }

    public async Task<IEnumerable<OrderItems>> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderItems
            .Where(o => o.OrderId.Id == orderId)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderItems entity)
    {
        _context.OrderItems.Update(entity);
        await Task.CompletedTask;
    }
}
