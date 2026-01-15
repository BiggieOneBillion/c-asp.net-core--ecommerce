using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.CreateProduct;

[HasPermission(Permissions.Products.Create)]
public record CreateProductCommand(
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice
) : ICommand<Guid>;
