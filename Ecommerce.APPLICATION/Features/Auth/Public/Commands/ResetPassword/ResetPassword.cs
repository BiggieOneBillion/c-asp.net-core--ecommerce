using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Interfaces;
using MediatR;
using Ecommerce.APPLICATION.ResponseDTOs;

namespace Ecommerce.APPLICATION.Features.Auth.Public.Commands.ResetPassword;

public record ResetPasswordCommand(string Token, string NewPassword) : IRequest<Result<GeneralResponse<Unit>>>;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashers _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHashers passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPasswordResetTokenAsync(command.Token);
        if (user == null || user.ResetTokenExpires < DateTime.UtcNow)
        {
            return Result.Failure<GeneralResponse<Unit>>(new Error("Error.NotFound", "Invalid or expired reset token."));
        }

        var passwordHash = _passwordHasher.HashPassword(command.NewPassword);
        user.UpdatePassword(passwordHash);

        // Revoke all existing refresh tokens for this user on password change (security requirement)
        user.RefreshTokens.Clear(); 

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Password reset successfully."));
    }
}
