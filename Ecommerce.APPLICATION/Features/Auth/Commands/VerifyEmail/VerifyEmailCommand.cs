using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(string Token, string Email) : IRequest<Result<GeneralResponse<AuthResponse>>>;
