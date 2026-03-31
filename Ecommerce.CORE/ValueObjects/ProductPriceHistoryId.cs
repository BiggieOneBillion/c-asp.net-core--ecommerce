using System;

namespace Ecommerce.CORE.ValueObjects;

public record ProductPriceHistoryId(Guid Id)
{
    public string Value() => Id.ToString();

    public static ProductPriceHistoryId Create(Guid id) => new(id);
}