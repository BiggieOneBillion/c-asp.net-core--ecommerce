using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderResponseDTO>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<OrderResponseDTO>> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId.Id);

            if (order == null)
            {
                return Result.Failure<OrderResponseDTO>(
                    new Error("Order.NotFound", $"Order with ID {request.OrderId} not found"));
            }

            var orderDto = _mapper.Map<OrderResponseDTO>(order);

            return Result.Success(orderDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<OrderResponseDTO>(
                new Error("Order.QueryFailed", $"Failed to retrieve order: {ex.Message}"));
        }
    }
}
