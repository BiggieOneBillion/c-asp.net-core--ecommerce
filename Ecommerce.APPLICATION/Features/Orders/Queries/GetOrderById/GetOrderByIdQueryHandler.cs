using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Order;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<CreateOrderDTO>>
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

    public async Task<Result<CreateOrderDTO>> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return Result.Failure<CreateOrderDTO>(
                    new Error("Order.NotFound", $"Order with ID {request.OrderId} not found"));
            }

            var orderDto = _mapper.Map<CreateOrderDTO>(order);

            return Result.Success(orderDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreateOrderDTO>(
                new Error("Order.QueryFailed", $"Failed to retrieve order: {ex.Message}"));
        }
    }
}
