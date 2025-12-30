using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.ProductPriceHistory;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Queries.GetPriceHistoryByProduct;

public class GetPriceHistoryByProductQueryHandler 
    : IRequestHandler<GetPriceHistoryByProductQuery, Result<PagedResult<CreateProductPriceHistoryDTO>>>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;
    private readonly IMapper _mapper;

    public GetPriceHistoryByProductQueryHandler(
        IProductPriceHistoryRepository priceHistoryRepository,
        IMapper mapper)
    {
        _priceHistoryRepository = priceHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<CreateProductPriceHistoryDTO>>> Handle(
        GetPriceHistoryByProductQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);
            var priceHistories = await _priceHistoryRepository.GetByProductIdAsync(productId);

            // Calculate pagination
            var totalCount = priceHistories.Count;
            var items = priceHistories
                .OrderByDescending(p => p.ChangedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var priceHistoryDtos = _mapper.Map<List<CreateProductPriceHistoryDTO>>(items);

            var pagedResult = new PagedResult<CreateProductPriceHistoryDTO>(
                priceHistoryDtos,
                request.PageNumber,
                request.PageSize,
                totalCount);

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Failure<PagedResult<CreateProductPriceHistoryDTO>>(
                new Error("ProductPriceHistory.QueryFailed", $"Failed to retrieve price history: {ex.Message}"));
        }
    }
}
