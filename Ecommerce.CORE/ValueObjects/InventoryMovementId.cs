using System;

namespace Ecommerce.CORE.ValueObjects;

public record InventoryMovementId(Guid Id)
{
    public string Value() => Id.ToString();

    public static InventoryMovementId Create(Guid id) => new(id);
}