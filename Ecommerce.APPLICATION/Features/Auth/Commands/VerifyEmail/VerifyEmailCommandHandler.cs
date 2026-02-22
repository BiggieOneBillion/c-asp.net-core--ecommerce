using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<GeneralResponse<AuthResponse>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyEmailCommandHandler(
        IUserRepository userRepository, 
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<AuthResponse>>> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByVerificationTokenAsync(command.Token);
        if (user == null || user.Email != command.Email)
        {
            return Result.Failure<GeneralResponse<AuthResponse>>(new Error("400", "Invalid or expired verification token."));
        }

        // 1. Verify email
        user.VerifyEmail();

        // 2. Generate tokens
        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        // 3. Track refresh token (Family tracking)
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            FamilyId = Guid.NewGuid().ToString("N"),
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            CreatedByIp = "Unknown" // Ideally passed through command
        };

        // 4. Track refresh token in repository
        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        // 5. Save changes in a single transaction
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse(
            accessToken,
            refreshToken,
            user.Name,
            user.Email,
            user.Role.ToString());

        return Result<GeneralResponse<AuthResponse>>.Success(GeneralResponse<AuthResponse>.CreateSuccess(response, "Email verified successfully. You are now logged in."));
    }
}
