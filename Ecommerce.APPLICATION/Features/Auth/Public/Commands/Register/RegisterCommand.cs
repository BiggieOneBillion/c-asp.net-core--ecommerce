using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Auth;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Auth.Public.Commands.Register;

public record RegisterCommand(RegisterRequest Request) : IRequest<Result<GeneralResponse<Unit>>>;
