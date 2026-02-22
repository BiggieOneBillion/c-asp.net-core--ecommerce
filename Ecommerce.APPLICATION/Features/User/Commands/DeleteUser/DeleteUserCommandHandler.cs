using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId.Id);

            if (user == null)
            {
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("User.NotFound", $"User with ID {request.UserId} not found"));
            }

            await _userRepository.DeleteAsync(user);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "User deleted successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("User.DeleteFailed", $"Failed to delete user: {ex.Message}"));
        }
    }
}
