using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Queries.GetInventoryByProduct;

public record GetInventoryByProductQuery(Guid ProductId) : IRequest<Result<InventoryResponseDTO>>;
