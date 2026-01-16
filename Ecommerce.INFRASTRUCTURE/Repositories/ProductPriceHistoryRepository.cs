using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data; // Assuming ApplicationDbContext is in this namespace
using Microsoft.EntityFrameworkCore; // Required for EF Core methods

namespace Ecommerce.INFRASTRUCTURE.Repositories
{
    public class ProductPriceHistoryRepository : IProductPriceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductPriceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProductPriceHistory entity)
        {
            await _context.ProductPriceHistories.AddAsync(entity);
        }

        public async Task DeleteAsync(ProductPriceHistory entity)
        {
            _context.ProductPriceHistories.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<ProductPriceHistory?> GetByIdAsync(Guid id)
        {
            return await _context.ProductPriceHistories.FirstOrDefaultAsync(p => p.Id.Id == id);
        }

        public async Task<IEnumerable<ProductPriceHistory>> GetAllAsync()
        {
            return await _context.ProductPriceHistories.ToListAsync();
        }

        public async Task<IEnumerable<ProductPriceHistory>> GetByProductIdAsync(Guid productId)
        {
            return await _context.ProductPriceHistories
                .Where(p => p.ProductId.Id == productId)
                .ToListAsync();
        }

        public async Task UpdateAsync(ProductPriceHistory entity)
        {
            _context.ProductPriceHistories.Update(entity);
            await Task.CompletedTask;
        }
    }
}