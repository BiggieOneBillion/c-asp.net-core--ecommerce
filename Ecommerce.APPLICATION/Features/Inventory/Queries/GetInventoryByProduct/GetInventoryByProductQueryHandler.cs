using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.Inventory;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;

public class GetInventoryByProductQueryHandler : IRequestHandler<GetInventoryByProductQuery, Result<CreateInventoryDTO>>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;

    public GetInventoryByProductQueryHandler(
        IInventoryRepository inventoryRepository,
        IMapper mapper)
    {
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateInventoryDTO>> Handle(
        GetInventoryByProductQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var inventory = await _inventoryRepository.GetByProductIdAsync(productId);

            if (inventory == null)
            {
                return Result.Failure<CreateInventoryDTO>(
                    new Error("Inventory.NotFound", $"Inventory for product {request.ProductId} not found"));
            }

            var inventoryDto = _mapper.Map<CreateInventoryDTO>(inventory);

            return Result.Success(inventoryDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<CreateInventoryDTO>(
                new Error("Inventory.QueryFailed", $"Failed to retrieve inventory: {ex.Message}"));
        }
    }
}
