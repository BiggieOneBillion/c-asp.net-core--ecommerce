using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.UpdateProduct;

[HasPermission(Permissions.Products.Update)]
public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId
) : ICommand;
