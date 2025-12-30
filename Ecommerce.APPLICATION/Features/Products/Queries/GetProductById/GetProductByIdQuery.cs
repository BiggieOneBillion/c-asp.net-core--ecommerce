using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IRequest<Result<ProductResponseDTO>>;
