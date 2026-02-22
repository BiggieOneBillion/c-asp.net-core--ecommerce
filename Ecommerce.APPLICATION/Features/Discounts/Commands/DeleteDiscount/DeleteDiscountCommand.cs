using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;

public record DeleteDiscountCommand(Guid Id) : IRequest<Result<GeneralResponse<Unit>>>;
