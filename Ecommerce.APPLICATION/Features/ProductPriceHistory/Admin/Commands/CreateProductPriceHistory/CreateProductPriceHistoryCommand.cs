using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Commands.CreateProductPriceHistory;

public record CreateProductPriceHistoryCommand(
    Guid ProductId,
    decimal NewPrice,
    decimal OldPrice,
    DateTime EffectiveDate,
    DateTime ChangedAt
) : IRequest<Result<GeneralResponse<Guid>>>;
