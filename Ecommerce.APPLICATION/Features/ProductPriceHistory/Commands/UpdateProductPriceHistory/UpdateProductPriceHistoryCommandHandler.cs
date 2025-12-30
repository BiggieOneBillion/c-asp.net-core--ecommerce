using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;

public class UpdateProductPriceHistoryCommandHandler : IRequestHandler<UpdateProductPriceHistoryCommand, Result>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;

    public UpdateProductPriceHistoryCommandHandler(IProductPriceHistoryRepository priceHistoryRepository)
    {
        _priceHistoryRepository = priceHistoryRepository;
    }

    public async Task<Result> Handle(
        UpdateProductPriceHistoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var priceHistoryId = ProductPriceHistoryId.Create(request.ProductPriceHistoryId);
            var priceHistory = await _priceHistoryRepository.GetByIdAsync(priceHistoryId);

            if (priceHistory == null)
            {
                return Result.Failure(
                    new Error("ProductPriceHistory.NotFound", $"Price history with ID {request.ProductPriceHistoryId} not found"));
            }

            var productId = ProductId.Create(request.ProductId);

            priceHistory.ProductId = productId;
            priceHistory.NewPrice = request.NewPrice;
            priceHistory.OldPrice = request.OldPrice;
            priceHistory.EffectiveDate = request.EffectiveDate;
            priceHistory.ChangedAt = request.ChangedAt;

            await _priceHistoryRepository.UpdateAsync(priceHistory);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(
                new Error("ProductPriceHistory.UpdateFailed", $"Failed to update price history: {ex.Message}"));
        }
    }
}
