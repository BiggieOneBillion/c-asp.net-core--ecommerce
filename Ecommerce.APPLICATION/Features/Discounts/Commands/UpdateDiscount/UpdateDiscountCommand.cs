using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;

public record UpdateDiscountCommand(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive
) : IRequest<Result<GeneralResponse<Unit>>>;
