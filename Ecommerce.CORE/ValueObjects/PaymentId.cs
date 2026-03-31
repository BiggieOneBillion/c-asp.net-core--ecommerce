using System;

namespace Ecommerce.CORE.ValueObjects;

public record PaymentId(Guid Id)
{
    public string Value() => Id.ToString();

    public static PaymentId Create(Guid id) => new(id);
}
