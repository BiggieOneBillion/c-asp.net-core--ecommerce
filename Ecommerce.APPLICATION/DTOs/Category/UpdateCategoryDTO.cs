using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Category
{
    public record UpdateCategoryDTO
    {
         public string CategoryName { get; init;} = string.Empty;

        public string CategoryDescription { get; init;} = string.Empty;

        public bool ActiveStatus {get; init;} = true;
    }
}