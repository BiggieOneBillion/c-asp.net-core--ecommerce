using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequest Request, string IpAddress) : IRequest<Result<AuthResponse>>;
