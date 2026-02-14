using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Command to update an existing product
/// </summary>
/// <param name="ProductId">Unique identifier of the product to update</param>
/// <param name="Name">Updated name of the product</param>
/// <param name="Description">Updated description of the product</param>
/// <param name="CategoryId">Unique identifier of the updated category</param>
[HasPermission(Permissions.Products.Update)]
public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId
) : ICommand;
