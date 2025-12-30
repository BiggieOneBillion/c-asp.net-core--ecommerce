using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    async public Task CreateAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);

    }

    public Task DeleteAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }
}
