using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice
) : ICommand;
