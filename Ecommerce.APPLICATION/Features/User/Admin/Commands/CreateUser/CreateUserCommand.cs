using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Admin.Commands.CreateUser;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password
) : IRequest<Result<GeneralResponse<Guid>>>;
