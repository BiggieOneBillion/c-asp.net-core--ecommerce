using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Commands.DeleteCategory;

/// <summary>
/// Command to delete a category
/// </summary>
/// <param name="Id">Category ID</param>
public record DeleteCategoryCommand(Guid Id) : IRequest<Result<GeneralResponse<Unit>>>;
