using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId) : ICommand;
