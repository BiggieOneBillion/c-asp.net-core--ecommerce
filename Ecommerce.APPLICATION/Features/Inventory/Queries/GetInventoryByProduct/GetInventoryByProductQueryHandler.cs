using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;

public class GetInventoryByProductQueryHandler : IRequestHandler<GetInventoryByProductQuery, Result<InventoryResponseDTO>>
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

    public async Task<Result<InventoryResponseDTO>> Handle(
        GetInventoryByProductQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var inventory = await _inventoryRepository.GetByProductIdAsync(productId.Id);

            if (inventory == null)
            {
                return Result.Failure<InventoryResponseDTO>(
                    new Error("Inventory.NotFound", $"Inventory for product {request.ProductId} not found"));
            }

            var inventoryDto = _mapper.Map<InventoryResponseDTO>(inventory);

            return Result.Success(inventoryDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<InventoryResponseDTO>(
                new Error("Inventory.QueryFailed", $"Failed to retrieve inventory: {ex.Message}"));
        }
    }
}
