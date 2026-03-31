using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Public.Queries.GetOrdersByUser;

public class GetOrdersByUserQueryHandler 
    : IRequestHandler<GetOrdersByUserQuery, Result<GeneralResponse<List<OrderResponseDTO>>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<List<OrderResponseDTO>>>> Handle(
        GetOrdersByUserQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetByUserIdAsync(request.UserId);
            var orderDtos = _mapper.Map<List<OrderResponseDTO>>(orders);

            return Result<GeneralResponse<List<OrderResponseDTO>>>.Success(
                GeneralResponse<List<OrderResponseDTO>>.CreateSuccess(orderDtos));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<List<OrderResponseDTO>>>(
                new Error("Order.QueryFailed", $"Failed to retrieve orders for user: {ex.Message}"));
        }
    }
}
