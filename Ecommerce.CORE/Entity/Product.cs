using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Product
{
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;

   public required CategoryId CategoryId { get; set; }

   public ProductId ProductId { get; set; } = new ProductId();

   public Product(string name, string description, Guid productId, Guid categoryId )
   {
      Name = name;
      Description = description;
      ProductId = ProductId.Create(productId);
      CategoryId = CategoryId.Create(categoryId);
   }
}
