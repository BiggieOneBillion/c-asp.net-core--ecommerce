using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories
{
    public class InventoryMovementRepository : IInventoryMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryMovementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(InventoryMovement entity)
        {
            await _context.InventoryMovements.AddAsync(entity);
        }

        public async Task DeleteAsync(InventoryMovement entity)
        {
            _context.InventoryMovements.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<InventoryMovement?> GetByIdAsync(Guid id)
        {
            return await _context.InventoryMovements.FirstOrDefaultAsync(i => i.Id.Id == id);
        }

        public async Task<IEnumerable<InventoryMovement>> GetAllAsync()
        {
            return await _context.InventoryMovements.ToListAsync();
        }

        public async Task<IEnumerable<InventoryMovement>> GetByProductIdAsync(Guid productId)
        {
            return await _context.InventoryMovements
                .Where(i => i.ProductId.Id == productId)
                .ToListAsync();
        }

        public async Task UpdateAsync(InventoryMovement entity)
        {
            _context.InventoryMovements.Update(entity);
            await Task.CompletedTask;
        }
    }
}