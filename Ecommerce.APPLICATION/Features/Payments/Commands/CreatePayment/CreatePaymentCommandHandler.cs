using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<Guid>>
{
    private readonly IPaymentRepository _paymentRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var paymentId = Guid.NewGuid();
            var orderId = OrderId.Create(request.OrderId);

            Payment payment = new(
                paymentType:request.PaymentType,
                amount:request.Amount,  
                orderId:orderId);

            await _paymentRepository.CreateAsync(payment);

            return Result.Success(paymentId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("Payment.CreateFailed", $"Failed to create payment: {ex.Message}"));
        }
    }
}
