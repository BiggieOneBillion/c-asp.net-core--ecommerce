using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.OrderItems;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Queries.GetOrderItemById;

public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Result<CreateOrderItemsDTO>>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IMapper _mapper;

    public GetOrderItemByIdQueryHandler(
        IOrderItemsRepository orderItemsRepository,
        IMapper mapper)
    {
        _orderItemsRepository = orderItemsRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateOrderItemsDTO>> Handle(
        GetOrderItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orderItemId = OrderItemsId.Create(request.OrderItemId);
            var orderItem = await _orderItemsRepository.GetByIdAsync(orderItemId);

            if (orderItem == null)
            {
                return Result.Failure<CreateOrderItemsDTO>(
                    new Error("OrderItem.NotFound", $"Order item with ID {request.OrderItemId} not found"));
            }

            var orderItemDto = _mapper.Map<CreateOrderItemsDTO>(orderItem);

            return Result.Success(orderItemDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreateOrderItemsDTO>(
                new Error("OrderItem.QueryFailed", $"Failed to retrieve order item: {ex.Message}"));
        }
    }
}
