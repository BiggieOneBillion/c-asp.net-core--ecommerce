using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetDiscountById;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, Result<GeneralResponse<DiscountResponseDTO>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<DiscountResponseDTO>>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);
        if (discount == null)
        {
            return Result.Failure<GeneralResponse<DiscountResponseDTO>>(new Error("Discount.NotFound", $"Discount with ID {request.Id} not found"));
        }

        var response = _mapper.Map<DiscountResponseDTO>(discount);
        return Result<GeneralResponse<DiscountResponseDTO>>.Success(GeneralResponse<DiscountResponseDTO>.CreateSuccess(response));
    }
}
