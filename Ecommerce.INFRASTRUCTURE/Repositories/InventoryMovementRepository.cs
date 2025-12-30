using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories
{
    public class InventoryMovementRepository : IInventoryMovementRepository
    {
        public Task CreateAsync(InventoryMovement entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(InventoryMovement entity)
        {
            throw new NotImplementedException();
        }

        public Task<InventoryMovement?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(InventoryMovement entity)
        {
            throw new NotImplementedException();
        }
    }
}