using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        // 1. Get user by email
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthResponse>.Failure("Invalid credentials.");
        }

        // 2. Check for account lockout
        if (user.IsLockedOut)
        {
            return Result<AuthResponse>.Failure($"Account is locked. Please try again after {user.LockoutEnd}.");
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
            return Result<AuthResponse>.Failure("Invalid credentials.");
        }

        // 4. Check if email is verified
        if (!user.IsEmailVerified)
        {
            return Result<AuthResponse>.Failure("Please verify your email before logging in.");
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

        user.RefreshTokens.Add(refreshTokenEntity);
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
