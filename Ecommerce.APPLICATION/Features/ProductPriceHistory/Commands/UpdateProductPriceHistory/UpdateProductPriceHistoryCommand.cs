using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Commands.UpdateProductPriceHistory;

public record UpdateProductPriceHistoryCommand(
    Guid ProductPriceHistoryId,
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt
) : IRequest<Result<GeneralResponse<Unit>>>;
