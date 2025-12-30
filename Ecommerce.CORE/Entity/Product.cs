using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Product
{
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;

   public  CategoryId CategoryId { get; set; }

   public decimal CurrentPrice { get; set; }

   public ProductId ProductId { get; set; } = new ProductId();

   public Product(string name, string description, Guid productId, CategoryId categoryId, decimal price )
   {
      Name = name;
      Description = description;
      ProductId = ProductId.Create(productId);
      CategoryId = categoryId;
      CurrentPrice = price;
   }
}
