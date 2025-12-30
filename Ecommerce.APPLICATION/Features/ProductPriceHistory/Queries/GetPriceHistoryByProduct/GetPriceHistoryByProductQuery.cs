using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.DTOs.ProductPriceHistory;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Queries.GetPriceHistoryByProduct;

public record GetPriceHistoryByProductQuery(
    Guid ProductId,
    int PageNumber = 1,
    int PageSize = 10
) : IQuery<PagedResult<CreateProductPriceHistoryDTO>>;
