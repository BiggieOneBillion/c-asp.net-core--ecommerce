using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus = true
) : ICommand<Guid>;
