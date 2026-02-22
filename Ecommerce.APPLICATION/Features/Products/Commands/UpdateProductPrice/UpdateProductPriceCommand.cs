using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProductPrice;

/// <summary>
/// Command to update a product's price with history tracking
/// </summary>
/// <param name="ProductId">Product ID</param>
/// <param name="NewPrice">The new price to set</param>
public record UpdateProductPriceCommand(
    Guid ProductId,
    decimal NewPrice
) : IRequest<Result<GeneralResponse<Unit>>>;
