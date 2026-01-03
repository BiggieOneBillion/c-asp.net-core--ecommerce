using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProductPrice;

public record UpdateProductPriceCommand(
    Guid ProductId,
    decimal NewPrice
) : ICommand<Guid>;
