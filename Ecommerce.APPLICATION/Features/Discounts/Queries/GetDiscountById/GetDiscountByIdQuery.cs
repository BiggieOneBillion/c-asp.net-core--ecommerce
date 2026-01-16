using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountById;

[HasPermission(Permissions.Discounts.View)]
public record GetDiscountByIdQuery(Guid Id) : IRequest<Result<DiscountResponseDTO>>;
