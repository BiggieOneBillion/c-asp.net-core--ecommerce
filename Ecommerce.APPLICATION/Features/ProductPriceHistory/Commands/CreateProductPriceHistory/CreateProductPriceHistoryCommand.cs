using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.CreateProductPriceHistory;

public record CreateProductPriceHistoryCommand(
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt
) : ICommand<Guid>;
