using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice
) : ICommand<Guid>;
