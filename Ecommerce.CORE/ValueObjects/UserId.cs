using System;

namespace Ecommerce.CORE.ValueObjects;

public readonly record struct UserId(Guid Id)
{
    public string Value() => Id.ToString();

    public static UserId Create(Guid id) => new UserId(id);
    
    public static implicit operator Guid(UserId userId) => userId.Id;
}
