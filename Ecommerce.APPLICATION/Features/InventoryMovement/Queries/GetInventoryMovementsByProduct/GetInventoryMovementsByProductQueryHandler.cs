using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Queries.GetInventoryMovementsByProduct;

public class GetInventoryMovementsByProductQueryHandler 
    : IRequestHandler<GetInventoryMovementsByProductQuery, Result<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>>
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

    public async Task<Result<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>> Handle(
        GetInventoryMovementsByProductQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var movements = (await _inventoryMovementRepository.GetByProductIdAsync(productId.Id)).ToList();

            if (movements == null || !movements.Any())
                return Result<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>.Success(
                    GeneralResponse<PagedResult<InventoryMovementResponseDTO>>.CreateSuccess(
                        new PagedResult<InventoryMovementResponseDTO>(new List<InventoryMovementResponseDTO>(), request.PageNumber, request.PageSize, 0),
                        "No inventory movements found for this product"));

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

            return Result<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>.Success(
                GeneralResponse<PagedResult<InventoryMovementResponseDTO>>.CreateSuccess(pagedResult));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<PagedResult<InventoryMovementResponseDTO>>>(
                new Error("InventoryMovement.QueryFailed", $"Failed to retrieve inventory movements: {ex.Message}"));
        }
    }
}
