using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Admin.Commands.UpdateProduct;

/// <summary>
/// Command to update an existing product's basic information
/// </summary>
public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId,
    string? ImageUrl,
    bool IsActive
) : IRequest<Result<GeneralResponse<Unit>>>;
