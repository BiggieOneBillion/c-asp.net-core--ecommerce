using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.APPLICATION.DTOs.Category
{
    /// <summary>
    /// DTO for updating an existing category
    /// </summary>
    public record UpdateCategoryDTO
    {
         /// <summary>
         /// Updated name of the category
         /// </summary>
         public string CategoryName { get; init;} = string.Empty;

        /// <summary>
        /// Updated description of the category
        /// </summary>
        public string CategoryDescription { get; init;} = string.Empty;

        /// <summary>
        /// Updated active status of the category
        /// </summary>
        public bool ActiveStatus {get; init;} = true;
    }
}