using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var token = command.Request.RefreshToken;

        // 1. Get refresh token from database
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
        if (refreshToken == null)
        {
            return Result<AuthResponse>.Failure("Invalid refresh token.");
        }

        // 2. Token Reuse Detection (Family Tracking)
        if (refreshToken.IsRevoked || refreshToken.IsUsed)
        {
            // Token family compromise detected! 
            // Revoke all tokens in this family
            await _refreshTokenRepository.RevokeFamilyAsync(refreshToken.FamilyId, command.IpAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<AuthResponse>.Failure("Invalid refresh token. Security compromise detected. All sessions revoked.");
        }

        // 3. Check expiration
        if (refreshToken.IsExpired)
        {
            return Result<AuthResponse>.Failure("Refresh token expired.");
        }

        // 4. Get User
        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            return Result<AuthResponse>.Failure("User not found.");
        }

        // 5. Rotate Token: Invalidate old token
        refreshToken.IsUsed = true;
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = command.IpAddress;

        // 6. Generate new tokens
        var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        // 7. Save new refresh token in the same family
        var newRefreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshToken,
            FamilyId = refreshToken.FamilyId,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedByIp = command.IpAddress
        };

        refreshToken.ReplacedByToken = newRefreshToken;
        user.RefreshTokens.Add(newRefreshTokenEntity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse(
            newAccessToken,
            newRefreshToken,
            user.Name,
            user.Email,
            user.Role.ToString());

        return Result<AuthResponse>.Success(response);
    }
}
