using System;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Category
{
    public CategoryId CategoryId { get; set; }

    public string CategoryName { get; set;} = string.Empty;

    public string CategoryDescription { get; set;} = string.Empty;

    public bool ActiveStatus {get; set;} = true;

    
}
