using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Commands.CreateProductPriceHistory;

public class CreateProductPriceHistoryCommandHandler : IRequestHandler<CreateProductPriceHistoryCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;

    public CreateProductPriceHistoryCommandHandler(IProductPriceHistoryRepository priceHistoryRepository)
    {
        _priceHistoryRepository = priceHistoryRepository;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(
        CreateProductPriceHistoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var productId = ProductId.Create(request.ProductId);

            var priceHistory = new CORE.Entity.ProductPriceHistory(
                productId:productId,
                newPrice:request.NewPrice,
                oldPrice:request.OldPrice,
                effectiveDate:request.EffectiveDate
                );

            await _priceHistoryRepository.CreateAsync(priceHistory);

            return Result<GeneralResponse<Guid>>.Success(
                GeneralResponse<Guid>.CreateSuccess(priceHistory.Id.Id, "Price history created successfully", 201));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Guid>>(
                new Error("ProductPriceHistory.CreateFailed", $"Failed to create price history: {ex.Message}"));
        }
    }
}
