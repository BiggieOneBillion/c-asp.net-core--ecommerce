using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Public.Commands.ResendVerification;

public record ResendVerificationCommand(string Email) : IRequest<Result<GeneralResponse<Unit>>>;

public class ResendVerificationHandler : IRequestHandler<ResendVerificationCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ResendVerificationHandler(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(ResendVerificationCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user == null || user.IsEmailVerified)
        {
            return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "If the email exists and is not verified, a new link has been sent."));
        }

        user.EmailVerificationToken = Guid.NewGuid().ToString("N");
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendVerificationEmailAsync(user.Email, user.Name, user.EmailVerificationToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "If the email exists and is not verified, a new link has been sent."));
    }
}
