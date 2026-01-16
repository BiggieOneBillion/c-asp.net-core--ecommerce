using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetActiveDiscounts;

[HasPermission(Permissions.Discounts.View)]
public record GetActiveDiscountsQuery() : IRequest<Result<List<DiscountResponseDTO>>>;
