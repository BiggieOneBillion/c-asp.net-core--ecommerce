using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Public.Queries.GetProductById;

/// <summary>
/// Query to get a product by its unique identifier
/// </summary>
/// <param name="Id">Product ID</param>
public record GetProductByIdQuery(Guid Id) : IRequest<Result<GeneralResponse<ProductResponseDTO>>>;
