using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.Features.Payments.Commands.CreatePayment;

[HasPermission(Permissions.Payments.Process)]
public record CreatePaymentCommand(
    PaymentType PaymentType,
    decimal Amount,
    DateTime PaymentDate,
    Guid OrderId
) : ICommand<Guid>;
