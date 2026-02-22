using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.UpdateCategory;

/// <summary>
/// Command to update an existing category's information
/// </summary>
public record UpdateCategoryCommand(
    Guid CategoryId,
    string Name,
    string Description,
    Guid? ParentCategoryId
) : IRequest<Result<GeneralResponse<Unit>>>;
