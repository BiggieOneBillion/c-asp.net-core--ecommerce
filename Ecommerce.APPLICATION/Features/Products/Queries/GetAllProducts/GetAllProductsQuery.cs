using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Query to get a paged list of all products
/// </summary>
/// <param name="PageNumber">Page number to retrieve (default: 1)</param>
/// <param name="PageSize">Number of items per page (default: 10)</param>
public record GetAllProductsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<ProductResponseDTO>>>;
