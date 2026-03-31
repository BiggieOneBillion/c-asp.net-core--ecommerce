using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetDiscountAnalytics;

public record GetDiscountAnalyticsQuery() : IRequest<Result<GeneralResponse<DiscountAnalyticsResponseDTO>>>;
