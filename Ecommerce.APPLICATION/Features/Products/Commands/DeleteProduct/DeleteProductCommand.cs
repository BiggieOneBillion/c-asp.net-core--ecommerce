using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.DeleteProduct;

[HasPermission(Permissions.Products.Delete)]
public record DeleteProductCommand(Guid ProductId) : ICommand;
