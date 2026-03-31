using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Public.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<Result<GeneralResponse<Unit>>>;
