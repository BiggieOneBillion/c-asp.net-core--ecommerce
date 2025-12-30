using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Users;

namespace Ecommerce.APPLICATION.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IQuery<CreateUserDTO>;
