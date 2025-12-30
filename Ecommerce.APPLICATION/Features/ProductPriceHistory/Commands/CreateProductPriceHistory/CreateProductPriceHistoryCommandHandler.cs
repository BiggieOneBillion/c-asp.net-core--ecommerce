using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.CreateProductPriceHistory;

public class CreateProductPriceHistoryCommandHandler : IRequestHandler<CreateProductPriceHistoryCommand, Result<Guid>>
{
    private readonly IProductPriceHistoryRepository _priceHistoryRepository;

    public CreateProductPriceHistoryCommandHandler(IProductPriceHistoryRepository priceHistoryRepository)
    {
        _priceHistoryRepository = priceHistoryRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductPriceHistoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var priceHistoryId = Guid.NewGuid();
            var productId = ProductId.Create(request.ProductId);

            var priceHistory = new CORE.Entity.ProductPriceHistory(
                priceHistoryId,
                productId,
                request.NewPrice,
                request.OldPrice,
                request.EffectiveDate,
                request.ChangedAt);

            await _priceHistoryRepository.CreateAsync(priceHistory);

            return Result.Success(priceHistoryId);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("ProductPriceHistory.CreateFailed", $"Failed to create price history: {ex.Message}"));
        }
    }
}
