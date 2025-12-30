using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<ProductResponseDTO>>>;
