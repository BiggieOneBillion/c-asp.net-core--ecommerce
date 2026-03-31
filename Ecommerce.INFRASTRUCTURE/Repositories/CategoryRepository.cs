using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
    }

    public async Task DeleteAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var categoryId = CategoryId.Create(id);
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await Task.CompletedTask;
    }
}
