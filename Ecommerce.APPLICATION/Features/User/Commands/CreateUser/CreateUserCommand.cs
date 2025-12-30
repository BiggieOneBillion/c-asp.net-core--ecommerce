using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password
) : ICommand<Guid>;
