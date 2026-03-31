using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentsByOrder;

public class GetPaymentsByOrderQueryHandler 
    : IRequestHandler<GetPaymentsByOrderQuery, Result<GeneralResponse<PagedResult<PaymentResponseDTO>>>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentsByOrderQueryHandler(
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<PagedResult<PaymentResponseDTO>>>> Handle(
        GetPaymentsByOrderQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var payments = (await _paymentRepository.GetByOrderIdAsync(orderId.Id)).ToList();

            if (payments == null || !payments.Any())
                return Result<GeneralResponse<PagedResult<PaymentResponseDTO>>>.Success(
                    GeneralResponse<PagedResult<PaymentResponseDTO>>.CreateSuccess(
                        new PagedResult<PaymentResponseDTO>(new List<PaymentResponseDTO>(), request.PageNumber, request.PageSize, 0),
                        "No payments found for this order"));

            // Calculate pagination
            var totalCount = payments.Count;
            var items = payments
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var paymentDtos = _mapper.Map<List<PaymentResponseDTO>>(items);

            var pagedResult = new PagedResult<PaymentResponseDTO>(
                paymentDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result<GeneralResponse<PagedResult<PaymentResponseDTO>>>.Success(
                GeneralResponse<PagedResult<PaymentResponseDTO>>.CreateSuccess(pagedResult));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<PagedResult<PaymentResponseDTO>>>(
                new Error("Payment.QueryFailed", $"Failed to retrieve payments: {ex.Message}"));
        }
    }
}
