using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Name,
    string Email,
    string? Password = null
) : IRequest<Result<GeneralResponse<Unit>>>;
