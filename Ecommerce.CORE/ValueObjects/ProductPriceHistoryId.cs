using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CORE.ValueObjects
{
    public class ProductPriceHistoryId
    {
         public Guid Id { get; set; }

         public string Value () => Id.ToString();

        public static ProductPriceHistoryId Create(Guid id)
        {
            return new ProductPriceHistoryId { Id = id };
        }
    }
}