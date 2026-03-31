using System;

namespace Ecommerce.CORE.ValueObjects;

public record InventoryId(Guid Id)
{
    public string Value() => Id.ToString();

    public static InventoryId Create(Guid id) => new(id);
}
