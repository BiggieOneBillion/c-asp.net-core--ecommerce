using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Payment;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<CreatePaymentDTO>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentByIdQueryHandler(
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreatePaymentDTO>> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var paymentId = PaymentId.Create(request.PaymentId);
            var payment = await _paymentRepository.GetByIdAsync(paymentId);

            if (payment == null)
            {
                return Result.Failure<CreatePaymentDTO>(
                    new Error("Payment.NotFound", $"Payment with ID {request.PaymentId} not found"));
            }

            var paymentDto = _mapper.Map<CreatePaymentDTO>(payment);

            return Result.Success(paymentDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreatePaymentDTO>(
                new Error("Payment.QueryFailed", $"Failed to retrieve payment: {ex.Message}"));
        }
    }
}
