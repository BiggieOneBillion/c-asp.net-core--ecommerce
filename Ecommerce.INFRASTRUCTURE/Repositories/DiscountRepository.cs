using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _context;

    public DiscountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Discount entity)
    {
        await _context.Discounts.AddAsync(entity);
    }

    public async Task<Discount?> GetByIdAsync(Guid id)
    {
        return await _context.Discounts.FindAsync(id);
    }

    public async Task<IEnumerable<Discount>> GetAllAsync()
    {
        return await _context.Discounts.ToListAsync();
    }

    public async Task UpdateAsync(Discount entity)
    {
        _context.Discounts.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Discount entity)
    {
        _context.Discounts.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Discount?> GetByCodeAsync(string code)
    {
        return await _context.Discounts
            .FirstOrDefaultAsync(d => d.Code == code && d.IsActive && d.EndDate >= DateTime.UtcNow);
    }

    public async Task<IEnumerable<Discount>> GetActiveDiscountsAsync()
    {
        return await _context.Discounts
            .Where(d => d.IsActive && d.StartDate <= DateTime.UtcNow && d.EndDate >= DateTime.UtcNow)
            .ToListAsync();
    }
}
