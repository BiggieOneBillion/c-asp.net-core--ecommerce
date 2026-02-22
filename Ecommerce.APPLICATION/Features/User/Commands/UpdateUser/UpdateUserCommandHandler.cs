using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.APPLICATION.Services;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateUserCommand request,
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

            // Check if email is being changed and if new email already exists
            if (user.Email != request.Email)
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return Result.Failure<GeneralResponse<Unit>>(
                        new Error("User.EmailExists", "A user with this email already exists"));
                }
            }

            user.Name = request.Name;
            user.Email = request.Email;

            // Update password only if provided
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = _passwordService.HashPassword(request.Password);
            }

            await _userRepository.UpdateAsync(user);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "User updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("User.UpdateFailed", $"Failed to update user: {ex.Message}"));
        }
    }
}
