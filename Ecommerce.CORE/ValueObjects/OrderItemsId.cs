using System;

namespace Ecommerce.CORE.ValueObjects;

public record OrderItemsId(Guid Id)
{
    public string Value() => Id.ToString();

    public static OrderItemsId Create(Guid id) => new(id);
}
