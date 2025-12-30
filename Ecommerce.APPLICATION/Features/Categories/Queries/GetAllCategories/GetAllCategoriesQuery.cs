using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Category;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    bool? ActiveOnly = null
) : IQuery<PagedResult<CreateCategoryDTO>>;
