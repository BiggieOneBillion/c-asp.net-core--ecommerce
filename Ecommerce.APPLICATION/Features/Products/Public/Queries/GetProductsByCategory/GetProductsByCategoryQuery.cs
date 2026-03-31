using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Public.Queries.GetProductsByCategory;

/// <summary>
/// Query to get products belonging to a specific category
/// </summary>
/// <param name="CategoryId">Category ID</param>
/// <param name="PageNumber">Page number to retrieve (default: 1)</param>
/// <param name="PageSize">Number of items per page (default: 10)</param>
public record GetProductsByCategoryQuery(
    Guid CategoryId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<GeneralResponse<PagedResult<ProductResponseDTO>>>>;
