using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<Result<GeneralResponse<Unit>>>;
