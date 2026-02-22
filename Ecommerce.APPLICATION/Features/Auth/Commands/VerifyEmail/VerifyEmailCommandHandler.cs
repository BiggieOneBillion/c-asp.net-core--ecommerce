using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
    {
        
        var user = await _userRepository.GetUserByVerificationTokenAsync(command.Token, command.Email);
        if (user == null)
        {
            return Result.Failure<Unit>(new Error("400", "Invalid or expired verification token."));
        }

        user.VerifyEmail();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}
