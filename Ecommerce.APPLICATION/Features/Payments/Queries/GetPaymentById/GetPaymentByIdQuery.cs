using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Queries.GetPaymentById;

public record GetPaymentByIdQuery(Guid PaymentId) : IRequest<Result<GeneralResponse<PaymentResponseDTO>>>;
