
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces
{
    public interface IInventoryMovementRepository : IRepository<InventoryMovement>
    {
       Task <IEnumerable<InventoryMovement>> GetByProductIdAsync(Guid productId);
    }
}