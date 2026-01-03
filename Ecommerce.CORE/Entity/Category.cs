using System;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Category : AggregateRoot<CategoryId>
{
    public string CategoryName { get; private set; } = string.Empty;
    public string CategoryDescription { get; private set; } = string.Empty;
    public bool ActiveStatus { get; private set; } = true;

    // Private constructor for EF Core
    private Category() { }

    // Factory method
    public static Category Create(string name, string description, bool activeStatus = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name is required");

        return new Category
        {
            Id = CategoryId.Create(Guid.NewGuid()),
            CategoryName = name,
            CategoryDescription = description,
            ActiveStatus = activeStatus
        };
    }

    public void UpdateDetails(string name, string description, bool activeStatus)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name is required");

        CategoryName = name;
        CategoryDescription = description;
        ActiveStatus = activeStatus;
    }
}
