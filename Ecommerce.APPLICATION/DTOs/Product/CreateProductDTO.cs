using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Product
{
    public record CreateProductDTO
    {
           public string Name { get; init; } = string.Empty;
           public string Description { get; init; } = string.Empty;

           public Guid CategoryId { get; init; }

           public decimal CurrentPrice { get; init; }

    }
}