using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<GeneralResponse<PaymentResponseDTO>>>
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

    public async Task<Result<GeneralResponse<PaymentResponseDTO>>> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var paymentId = PaymentId.Create(request.PaymentId);
            var payment = await _paymentRepository.GetByIdAsync(paymentId.Id);

            if (payment == null)
            {
                return Result.Failure<GeneralResponse<PaymentResponseDTO>>(
                    new Error("Payment.NotFound", $"Payment with ID {request.PaymentId} not found"));
            }

            var paymentDto = _mapper.Map<PaymentResponseDTO>(payment);

            return Result<GeneralResponse<PaymentResponseDTO>>.Success(
                GeneralResponse<PaymentResponseDTO>.CreateSuccess(paymentDto));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<PaymentResponseDTO>>(
                new Error("Payment.QueryFailed", $"Failed to retrieve payment: {ex.Message}"));
        }
    }
}
