using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

/// <summary>
/// Command to create a new product
/// </summary>
/// <param name="Name">Name of the product</param>
/// <param name="Description">Detailed description of the product</param>
/// <param name="CategoryId">Unique identifier of the category this product belongs to</param>
/// <param name="CurrentPrice">Initial price of the product</param>
[HasPermission(Permissions.Products.Create)]
public record CreateProductCommand(
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice
) : ICommand<Guid>;
