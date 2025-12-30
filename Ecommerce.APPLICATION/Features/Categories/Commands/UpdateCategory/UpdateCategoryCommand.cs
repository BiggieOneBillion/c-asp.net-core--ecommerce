using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus
) : ICommand;
