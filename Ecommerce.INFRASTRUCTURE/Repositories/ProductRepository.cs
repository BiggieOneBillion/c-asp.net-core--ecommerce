using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
    }

    public async Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
       
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        var catId = CategoryId.Create(categoryId);
        return await _context.Products
            .Where(p => p.CategoryId == catId)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var productId = ProductId.Create(id);
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product?> GetProductByNameAsync(string productName)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == productName);
    }

    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await Task.CompletedTask;
    }

    public async Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var productIds = ids.Select(ProductId.Create).ToList();
        return await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
    }
}
