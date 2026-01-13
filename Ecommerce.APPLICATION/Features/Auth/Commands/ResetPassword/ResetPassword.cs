using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(string Token, string NewPassword) : IRequest<Result<Unit>>;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByPasswordResetTokenAsync(command.Token);
        if (user == null || user.ResetTokenExpires < DateTime.UtcNow)
        {
            return Result<Unit>.Failure("Invalid or expired reset token.");
        }

        var passwordHash = _passwordHasher.HashPassword(command.NewPassword);
        user.UpdatePassword(passwordHash);

        // Revoke all existing refresh tokens for this user on password change (security requirement)
        user.RefreshTokens.Clear(); 

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
