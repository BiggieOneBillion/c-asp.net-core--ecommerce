using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemsByOrder;

public class GetOrderItemsByOrderQueryHandler 
    : IRequestHandler<GetOrderItemsByOrderQuery, Result<PagedResult<OrderItemResponseDTO>>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IMapper _mapper;

    public GetOrderItemsByOrderQueryHandler(
        IOrderItemsRepository orderItemsRepository,
        IMapper mapper)
    {
        _orderItemsRepository = orderItemsRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<OrderItemResponseDTO>>> Handle(
        GetOrderItemsByOrderQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderId = OrderId.Create(request.OrderId);
            var orderItems = (await _orderItemsRepository.GetByOrderIdAsync(orderId.Id)).ToList();

            // Calculate pagination
            var totalCount = orderItems.Count;
            var items = orderItems
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var orderItemDtos = _mapper.Map<List<OrderItemResponseDTO>>(items);

            var pagedResult = new PagedResult<OrderItemResponseDTO>(
                orderItemDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<OrderItemResponseDTO>>(
                new Error("OrderItem.QueryFailed", $"Failed to retrieve order items: {ex.Message}"));
        }
    }
}
