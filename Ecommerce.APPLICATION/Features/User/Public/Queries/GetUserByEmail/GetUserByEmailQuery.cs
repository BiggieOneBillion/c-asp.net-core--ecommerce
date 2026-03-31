using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Users.Public.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<Result<GeneralResponse<UserResponseDTO>>>;
