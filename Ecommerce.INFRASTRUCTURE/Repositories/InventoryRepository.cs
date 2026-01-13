using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Inventory entity)
    {
        await _context.Inventories.AddAsync(entity);
    }

    public async Task DeleteAsync(Inventory entity)
    {
        _context.Inventories.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Inventory?> GetByIdAsync(Guid id)
    {
        return await _context.Inventories.FirstOrDefaultAsync(i => i.Id.Id == id);
    }

    public async Task<Inventory> GetByProductIdAsync(Guid productId)
    {
        return await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == ProductId.Create(productId)) ?? null!;
    }

    public async Task UpdateAsync(Inventory entity)
    {
        _context.Inventories.Update(entity);
        await Task.CompletedTask;
    }
}
