using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Public.Commands.Refresh;

public record RefreshTokenCommand(RefreshTokenRequest Request, string IpAddress) : IRequest<Result<GeneralResponse<AuthResponse>>>;
