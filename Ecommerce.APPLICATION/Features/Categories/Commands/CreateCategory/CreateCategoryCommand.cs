using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.CreateCategory;

/// <summary>
/// Command to create a new product category
/// </summary>
public record CreateCategoryCommand(
    string Name,
    string Description,
    Guid? ParentCategoryId
) : IRequest<Result<GeneralResponse<Guid>>>;
