using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentsByOrder;

public record GetPaymentsByOrderQuery(
    Guid OrderId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<GeneralResponse<PagedResult<PaymentResponseDTO>>>>;
