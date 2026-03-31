using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Inventory.Admin.Queries.GetInventoryByProduct;

public record GetInventoryByProductQuery(Guid ProductId) : IRequest<Result<GeneralResponse<InventoryResponseDTO>>>;
