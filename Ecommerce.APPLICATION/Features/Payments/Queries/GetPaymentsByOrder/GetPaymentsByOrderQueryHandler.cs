using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Payment;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Payments.Queries.GetPaymentsByOrder;

public class GetPaymentsByOrderQueryHandler 
    : IRequestHandler<GetPaymentsByOrderQuery, Result<PagedResult<CreatePaymentDTO>>>
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

    public async Task<Result<PagedResult<CreatePaymentDTO>>> Handle(
        GetPaymentsByOrderQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var payments = await _paymentRepository.GetByOrderIdAsync(orderId);

            // Calculate pagination
            var totalCount = payments.Count;
            var items = payments
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var paymentDtos = _mapper.Map<List<CreatePaymentDTO>>(items);

            var pagedResult = new PagedResult<CreatePaymentDTO>(
                paymentDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<CreatePaymentDTO>>(
                new Error("Payment.QueryFailed", $"Failed to retrieve payments: {ex.Message}"));
        }
    }
}
