using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Orders.Queries.GetOrdersByUser;

public class GetOrdersByUserQueryHandler 
    : IRequestHandler<GetOrdersByUserQuery, Result<PagedResult<OrderResponseDTO>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByUserQueryHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<OrderResponseDTO>>> Handle(
        GetOrdersByUserQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Create(request.UserId);
            var orders = (await _orderRepository.GetByUserIdAsync(userId.Id)).ToList();

            // Calculate pagination
            var totalCount = orders.Count;
            var items = orders
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var orderDtos = _mapper.Map<List<OrderResponseDTO>>(items);

            var pagedResult = new PagedResult<OrderResponseDTO>(
                orderDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<OrderResponseDTO>>(
                new Error("Order.QueryFailed", $"Failed to retrieve orders: {ex.Message}"));
        }
    }
}
