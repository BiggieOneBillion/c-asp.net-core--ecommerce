using System;

namespace Ecommerce.CORE.Entity;

public record ProductId(Guid Id)
{
    public string Value() => Id.ToString();

    public static ProductId Create(Guid id) => new(id);
}
