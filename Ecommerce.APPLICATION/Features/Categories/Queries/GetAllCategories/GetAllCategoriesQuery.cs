using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetAllCategories;

/// <summary>
/// Query to retrieve all product categories
/// </summary>
public record GetAllCategoriesQuery() : IRequest<Result<GeneralResponse<List<CategoryResponseDTO>>>>;
