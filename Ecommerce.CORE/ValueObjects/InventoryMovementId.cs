using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CORE.ValueObjects
{
    public class InventoryMovementId
    {
        public Guid Id { get; set; }

        public string Value () => Id.ToString();

        public static InventoryMovementId Create(Guid id)
        {
            return new InventoryMovementId { Id = id };
        }
    }
}