using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Name,
    string Email,
    string? Password = null
) : ICommand;
