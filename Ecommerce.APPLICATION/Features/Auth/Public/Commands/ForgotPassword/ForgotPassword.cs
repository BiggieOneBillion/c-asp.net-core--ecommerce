using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Interfaces;
using MediatR;
using Ecommerce.APPLICATION.ResponseDTOs;

namespace Ecommerce.APPLICATION.Features.Auth.Public.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<Result<GeneralResponse<Unit>>>;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user == null)
        {
            // For security, don't reveal if user exists. Return success regardless.
            return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "A reset link has been sent to the email."));
        }

        user.PasswordResetToken = Guid.NewGuid().ToString("N");
        user.ResetTokenExpires = DateTime.UtcNow.AddHours(24);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _emailService.SendPasswordResetEmailAsync(user.Email, user.Name, user.PasswordResetToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "A reset link has been sent to the email."));
    }
}
