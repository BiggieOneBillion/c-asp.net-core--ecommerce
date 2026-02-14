using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.CreateCategory;

/// <summary>
/// Command to create a new category
/// </summary>
/// <param name="CategoryName">Name of the category</param>
/// <param name="CategoryDescription">Detailed description of the category</param>
/// <param name="ActiveStatus">Status of the category (default: true)</param>
[HasPermission(Permissions.Categories.Create)]
public record CreateCategoryCommand(
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus = true
) : ICommand<Guid>;
