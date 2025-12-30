using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.DTOs.Inventory;

namespace Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;

public record GetInventoryByProductQuery(Guid ProductId) : IQuery<CreateInventoryDTO>;
