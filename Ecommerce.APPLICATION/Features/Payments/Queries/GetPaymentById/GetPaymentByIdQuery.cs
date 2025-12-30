using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Payment;

namespace Ecommerce.APPLICATION.Features.Payments.Queries.GetPaymentById;

public record GetPaymentByIdQuery(Guid PaymentId) : IQuery<CreatePaymentDTO>;
