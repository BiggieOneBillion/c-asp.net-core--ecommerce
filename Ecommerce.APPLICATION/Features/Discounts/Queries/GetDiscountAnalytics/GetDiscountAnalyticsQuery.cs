using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountAnalytics;

[HasPermission(Permissions.Discounts.Manage)]
public record GetDiscountAnalyticsQuery() : IRequest<Ecommerce.APPLICATION.Common.Models.Result<DiscountAnalyticsResponseDTO>>;
