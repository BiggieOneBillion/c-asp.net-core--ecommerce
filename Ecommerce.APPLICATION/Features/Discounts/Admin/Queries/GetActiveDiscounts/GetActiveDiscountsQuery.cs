using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetActiveDiscounts;

[HasPermission(Permissions.Discounts.View)]
public record GetActiveDiscountsQuery(int page = 1) : IRequest<Result<GeneralResponse<List<DiscountResponseDTO>>>>;
