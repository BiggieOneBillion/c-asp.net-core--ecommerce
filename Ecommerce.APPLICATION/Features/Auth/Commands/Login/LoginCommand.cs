using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequest Request, string IpAddress) : IRequest<Result<GeneralResponse<AuthResponse>>>;
