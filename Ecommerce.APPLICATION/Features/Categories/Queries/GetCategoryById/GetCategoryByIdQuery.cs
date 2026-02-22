using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;

/// <summary>
/// Query to retrieve a category by its unique identifier
/// </summary>
/// <param name="Id">Category ID</param>
public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<GeneralResponse<CategoryResponseDTO>>>;
