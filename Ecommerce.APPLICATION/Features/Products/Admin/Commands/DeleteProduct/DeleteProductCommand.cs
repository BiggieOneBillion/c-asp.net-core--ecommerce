using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Admin.Commands.DeleteProduct;

/// <summary>
/// Command to delete a product
/// </summary>
/// <param name="Id">Product ID</param>
public record DeleteProductCommand(Guid Id) : IRequest<Result<GeneralResponse<Unit>>>;
