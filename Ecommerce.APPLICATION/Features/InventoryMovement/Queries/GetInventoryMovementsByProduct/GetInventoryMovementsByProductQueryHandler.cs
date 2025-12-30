using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Queries.GetInventoryMovementsByProduct;

public class GetInventoryMovementsByProductQueryHandler 
    : IRequestHandler<GetInventoryMovementsByProductQuery, Result<PagedResult<InventoryMovementResponseDTO>>>
{
    private readonly IInventoryMovementRepository _inventoryMovementRepository;
    private readonly IMapper _mapper;

    public GetInventoryMovementsByProductQueryHandler(
        IInventoryMovementRepository inventoryMovementRepository,
        IMapper mapper)
    {
        _inventoryMovementRepository = inventoryMovementRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<InventoryMovementResponseDTO>>> Handle(
        GetInventoryMovementsByProductQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var movements = (await _inventoryMovementRepository.GetByProductIdAsync(productId.Id)).ToList();

            // Calculate pagination
            var totalCount = movements.Count;
            var items = movements
                .OrderByDescending(m => m.Timestamp)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var movementDtos = _mapper.Map<List<InventoryMovementResponseDTO>>(items);

            var pagedResult = new PagedResult<InventoryMovementResponseDTO>(
                movementDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<InventoryMovementResponseDTO>>(
                new Error("InventoryMovement.QueryFailed", $"Failed to retrieve inventory movements: {ex.Message}"));
        }
    }
}
