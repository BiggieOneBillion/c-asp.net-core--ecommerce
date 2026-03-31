using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Public.Queries.GetOrderById;

public class GetOrderByIdQueryHandler 
    : IRequestHandler<GetOrderByIdQuery, Result<GeneralResponse<OrderResponseDTO>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<OrderResponseDTO>>> Handle(
        GetOrderByIdQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return Result.Failure<GeneralResponse<OrderResponseDTO>>(
                    new Error("Order.NotFound", $"Order with ID {request.Id} not found"));

            var orderDto = _mapper.Map<OrderResponseDTO>(order);

            return Result<GeneralResponse<OrderResponseDTO>>.Success(
                GeneralResponse<OrderResponseDTO>.CreateSuccess(orderDto));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<OrderResponseDTO>>(
                new Error("Order.QueryFailed", $"Failed to retrieve order: {ex.Message}"));
        }
    }
}
