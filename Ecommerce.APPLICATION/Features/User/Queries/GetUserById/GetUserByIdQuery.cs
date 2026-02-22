using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<GeneralResponse<UserResponseDTO>>>;
