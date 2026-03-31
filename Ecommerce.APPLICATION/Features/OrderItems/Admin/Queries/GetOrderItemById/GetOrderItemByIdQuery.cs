using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Queries.GetOrderItemById;

public record GetOrderItemByIdQuery(Guid OrderItemId) : IRequest<Result<GeneralResponse<OrderItemResponseDTO>>>;
