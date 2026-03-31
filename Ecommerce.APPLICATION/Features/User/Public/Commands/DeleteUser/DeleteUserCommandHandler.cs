using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Public.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(
        IUserRepository userRepository, 
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
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

            // 1. Soft delete the user
            user.SoftDelete();
            await _userRepository.UpdateAsync(user);

            // 2. Revoke all tokens for the user
            await _refreshTokenRepository.RevokeAllUserTokensAsync(userId, "System (User Deleted)");

            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
