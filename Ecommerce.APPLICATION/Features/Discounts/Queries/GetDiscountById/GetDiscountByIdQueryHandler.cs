using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountById;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, Result<DiscountResponseDTO>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<DiscountResponseDTO>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var discount = await _discountRepository.GetByIdAsync(request.Id);
            if (discount == null)
            {
                return Result.Failure<DiscountResponseDTO>(new Error("Discount.NotFound", $"Discount with ID {request.Id} not found"));
            }

            var discountDto = _mapper.Map<DiscountResponseDTO>(discount);
            return Result.Success(discountDto);
        }
        catch (Exception ex)
        {
            return Result.Failure<DiscountResponseDTO>(new Error("Discount.QueryFailed", $"Failed to retrieve discount: {ex.Message}"));
        }
    }
}
