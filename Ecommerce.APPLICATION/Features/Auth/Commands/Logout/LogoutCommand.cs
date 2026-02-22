using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken, string AccessToken) : IRequest<Result<GeneralResponse<Unit>>>;
