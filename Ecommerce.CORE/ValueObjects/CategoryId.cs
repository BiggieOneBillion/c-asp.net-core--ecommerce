using System;

namespace Ecommerce.CORE.ValueObjects;

public record CategoryId(Guid Id)
{
    public string Value() => Id.ToString();

    public static CategoryId Create(Guid id) => new(id);
}
