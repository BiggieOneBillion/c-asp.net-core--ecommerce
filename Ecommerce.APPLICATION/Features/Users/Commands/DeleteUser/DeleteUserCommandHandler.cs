using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return Result.Failure(
                    new Error("User.NotFound", $"User with ID {request.UserId} not found"));
            }

            await _userRepository.DeleteAsync(userId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("User.DeleteFailed", $"Failed to delete user: {ex.Message}"));
        }
    }
}
