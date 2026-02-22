using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountById;

public record GetDiscountByIdQuery(Guid Id) : IRequest<Result<GeneralResponse<DiscountResponseDTO>>>;
