using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.VerifyEmail;

public record VerifyEmailCommand(string Token) : IRequest<Result<Unit>>;
