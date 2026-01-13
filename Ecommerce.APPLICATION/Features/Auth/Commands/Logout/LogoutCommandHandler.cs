using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<Unit>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenBlacklistService _tokenBlacklistService;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        ITokenBlacklistService tokenBlacklistService,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenBlacklistService = tokenBlacklistService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        // 1. Revoke the refresh token
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(command.RefreshToken);
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // 2. Blacklist the access token
        // We'll use a fixed expiry (e.g. 15 mins) for the blacklist since access tokens expire in 15 mins
        await _tokenBlacklistService.BlacklistTokenAsync(command.AccessToken, TimeSpan.FromMinutes(15));

        return Result<Unit>.Success(Unit.Value);
    }
}
