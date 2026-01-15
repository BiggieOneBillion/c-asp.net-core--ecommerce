using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashers _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordHashers passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<Result<AuthResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        // 1. Check if user already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Result.Failure<AuthResponse>(new Error("409", "User with this email already exists."));
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

        // 6. Generate tokens
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        // 7. Track refresh token (Family tracking)
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            FamilyId = Guid.NewGuid().ToString("N"),
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedByIp = "Unknown" // Should be passed from controller or context
        };

        // 8. Track refresh token in repository
        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        // 9. Save user with refresh token in a single transaction
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 10. Send verification email (after successful save)
        await _emailService.SendVerificationEmailAsync(user.Email, user.Name, user.EmailVerificationToken);

        var response = new AuthResponse(
            AccessToken:accessToken,
            RefreshToken:refreshToken,
            Name:user.Name,
            Email:user.Email,
            Role:user.Role.ToString());

        return Result<AuthResponse>.Success(response);
    }
}
