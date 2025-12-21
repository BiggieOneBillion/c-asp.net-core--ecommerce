using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task AddAsync(Category entity)
    {
        throw new NotImplementedException();
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
