using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;
