using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Product;

namespace Ecommerce.APPLICATION.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<CreateProductDTO>;
