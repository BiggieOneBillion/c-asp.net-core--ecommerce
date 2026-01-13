using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Refresh;

public record RefreshTokenCommand(RefreshTokenRequest Request, string IpAddress) : IRequest<Result<AuthResponse>>;
