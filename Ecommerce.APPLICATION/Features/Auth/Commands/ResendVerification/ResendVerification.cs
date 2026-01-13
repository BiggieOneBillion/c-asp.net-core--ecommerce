using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.ResendVerification;

public record ResendVerificationCommand(string Email) : IRequest<Result<Unit>>;

public class ResendVerificationCommandHandler : IRequestHandler<ResendVerificationCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ResendVerificationCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(ResendVerificationCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user == null || user.IsEmailVerified)
        {
            // For security/privacy, return success even if user doesn't exist or is already verified
            return Result<Unit>.Success(Unit.Value);
        }

        user.EmailVerificationToken = Guid.NewGuid().ToString("N");
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _emailService.SendVerificationEmailAsync(user.Email, user.Name, user.EmailVerificationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
