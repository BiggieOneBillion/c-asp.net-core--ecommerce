using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashers _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordHashers passwordHasher,
        IUnitOfWork unitOfWork,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        // 1. Check if user already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Result.Failure<GeneralResponse<Unit>>(new Error("409", "User with this email already exists."));
        }

        // 2. Hash password
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // 3. Create user
        var user = new CORE.Entity.Users(
            request.Name,
            request.Email,
            passwordHash,
            Guid.NewGuid());

        // 4. Generate email verification token
        user.EmailVerificationToken = Guid.NewGuid().ToString("N");

        // 5. Add user to repository (not saved yet)
        await _userRepository.CreateAsync(user);

        // 6. Save user in a single transaction
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 7. Send verification email (after successful save)
        await _emailService.SendVerificationEmailAsync(user.Email, user.Name, user.EmailVerificationToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "User registered successfully. Please check your email for the verification code.", 201));
    }
}
