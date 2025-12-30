using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Category;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid CategoryId) : IQuery<CreateCategoryDTO>;
