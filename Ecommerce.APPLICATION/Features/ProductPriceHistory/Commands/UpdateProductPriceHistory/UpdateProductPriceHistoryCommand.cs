using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;

public record UpdateProductPriceHistoryCommand(
    Guid ProductPriceHistoryId,
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt
) : ICommand;
