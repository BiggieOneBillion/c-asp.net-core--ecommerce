using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;

public class UpdateProductPriceHistoryCommandHandler : IRequestHandler<UpdateProductPriceHistoryCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;

    public UpdateProductPriceHistoryCommandHandler(IProductPriceHistoryRepository priceHistoryRepository)
    {
        _priceHistoryRepository = priceHistoryRepository;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(
        UpdateProductPriceHistoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var priceHistoryId = ProductPriceHistoryId.Create(request.ProductPriceHistoryId);
            var priceHistory = await _priceHistoryRepository.GetByIdAsync(priceHistoryId.Id);

            if (priceHistory == null)
            {
                return Result.Failure<GeneralResponse<Unit>>(
                    new Error("ProductPriceHistory.NotFound", $"Price history with ID {request.ProductPriceHistoryId} not found"));
            }

            var productId = ProductId.Create(request.ProductId);

            priceHistory.UpdateDetails(productId, request.NewPrice, request.OldPrice, request.EffectiveDate, request.ChangedAt);

            await _priceHistoryRepository.UpdateAsync(priceHistory);

            return Result<GeneralResponse<Unit>>.Success(
                GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Price history updated successfully"));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<Unit>>(
                new Error("ProductPriceHistory.UpdateFailed", $"Failed to update price history: {ex.Message}"));
        }
    }
}
