using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterRequest Request) : IRequest<Result<AuthResponse>>;
