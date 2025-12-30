using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Users;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<CreateUserDTO>;
