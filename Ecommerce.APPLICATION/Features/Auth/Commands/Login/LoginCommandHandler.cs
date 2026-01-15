using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
// using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashers _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHashers passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        // 1. Get user by email
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure<AuthResponse>(
                new Error("401", "Invalid credentials."));
        }

        // 2. Check for account lockout
        if (user.IsLockedOut)
        {
            return Result.Failure<AuthResponse>(
                new Error("403", $"Account is locked. Please try again after {user.LockoutEnd}."));
        }

        // 3. Verify password
        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            user.AccessFailedCount++;
            if (user.AccessFailedCount >= 5)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Failure<AuthResponse>(
                new Error("401", "Invalid credentials."));
        }

        // 4. Check if email is verified
        if (!user.IsEmailVerified)
        {
            return Result.Failure<AuthResponse>(
                new Error("403", "Please verify your email before logging in."));
        }

        // 5. Reset failed access count on successful login
        user.AccessFailedCount = 0;
        user.LockoutEnd = null;

        // 6. Generate tokens
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        // 7. Track refresh token (Family tracking)
        var refreshTokenEntity = new CORE.Entity.RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            FamilyId = Guid.NewGuid().ToString("N"),
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedByIp = command.IpAddress
        };

        // Note: Using repository instead of user.RefreshTokens.Add to avoid EF Core tracking issues
        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);   

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse(
            accessToken,
            refreshToken,
            user.Name,
            user.Email,
            user.Role.ToString());

        return Result<AuthResponse>.Success(response);
    }
}
