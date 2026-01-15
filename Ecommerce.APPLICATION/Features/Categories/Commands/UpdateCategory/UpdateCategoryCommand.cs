using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.UpdateCategory;

[HasPermission(Permissions.Categories.Update)]
public record UpdateCategoryCommand(
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus
) : ICommand;
