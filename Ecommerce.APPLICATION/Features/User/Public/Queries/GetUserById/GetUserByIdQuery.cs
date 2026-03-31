using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Public.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<GeneralResponse<UserResponseDTO>>>;
