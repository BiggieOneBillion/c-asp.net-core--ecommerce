using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid CategoryId) : IRequest<Result<CategoryResponseDTO>>;
