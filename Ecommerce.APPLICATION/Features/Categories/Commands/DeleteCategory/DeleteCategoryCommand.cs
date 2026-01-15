using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.DeleteCategory;

[HasPermission(Permissions.Categories.Delete)]
public record DeleteCategoryCommand(Guid CategoryId) : ICommand;
