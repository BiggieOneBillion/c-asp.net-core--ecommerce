using System;

namespace Ecommerce.CORE.ValueObjects;

public record OrderId(Guid Id)
{
    public string Value() => Id.ToString();

    public static OrderId Create(Guid id) => new(id);
}
