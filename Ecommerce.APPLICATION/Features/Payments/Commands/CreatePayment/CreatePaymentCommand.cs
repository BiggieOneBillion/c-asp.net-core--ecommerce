using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Enums;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Commands.CreatePayment;

[HasPermission(Permissions.Payments.Process)]
public record CreatePaymentCommand(
    PaymentType PaymentType,
    decimal Amount,
    DateTime PaymentDate,
    Guid OrderId
) : IRequest<Result<GeneralResponse<Guid>>>;
