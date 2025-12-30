using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.Features.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(
    PaymentType PaymentType,
    decimal Amount,
    DateTime PaymentDate,
    Guid OrderId
) : ICommand<Guid>;
