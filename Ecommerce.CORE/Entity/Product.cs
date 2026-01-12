using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get;  set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    public CategoryId CategoryId { get;  set; } = null!;
    public decimal CurrentPrice { get;  set; }
    
    // Private constructor for EF Core
    private Product() { }
    
    // Factory method
    public static Product Create(string name, string description, CategoryId categoryId, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name is required");
        
        if (price <= 0)
            throw new DomainException("Product price must be greater than zero");
        
        Product product =  new Product
        {
            Id = ProductId.Create(Guid.NewGuid()),
            Name = name,
            Description = description,
            CategoryId = categoryId,
            CurrentPrice = price
        };

        product.RaiseDomainEvent(new ProductCreatedDomainEvent(
            Guid.Parse(product.Id.Value()), 
            0 // Initial stock quantity is 0
        ));

        return product;
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new DomainException("Price must be greater than zero");
        
        var oldPrice = CurrentPrice;
        CurrentPrice = newPrice;
        
        RaiseDomainEvent(new ProductPriceChangedDomainEvent(
            Guid.Parse(Id.Value()), 
            oldPrice, 
            newPrice
        ));
    }
}
