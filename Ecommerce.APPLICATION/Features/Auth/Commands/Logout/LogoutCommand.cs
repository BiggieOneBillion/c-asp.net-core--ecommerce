using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken, string AccessToken) : IRequest<Result<Unit>>;
