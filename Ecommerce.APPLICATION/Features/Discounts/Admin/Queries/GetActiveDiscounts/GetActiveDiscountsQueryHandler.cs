using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Queries.GetActiveDiscounts;

public class GetActiveDiscountsQueryHandler : IRequestHandler<GetActiveDiscountsQuery, Result<GeneralResponse<List<DiscountResponseDTO>>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetActiveDiscountsQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<GeneralResponse<List<DiscountResponseDTO>>>> Handle(GetActiveDiscountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var activeDiscounts = await _discountRepository.GetActiveDiscountsAsync();

            if (activeDiscounts == null)
            {
                return Result.Failure<GeneralResponse<List<DiscountResponseDTO>>>(new Error("Discount.QueryFailed", $"No active discounts"));
            }
            var discountDtos = _mapper.Map<List<DiscountResponseDTO>>(activeDiscounts);
            return Result<GeneralResponse<List<DiscountResponseDTO>>>.Success(GeneralResponse<List<DiscountResponseDTO>>.CreateSuccess(discountDtos));
        }
        catch (Exception ex)
        {
            return Result.Failure<GeneralResponse<List<DiscountResponseDTO>>>(new Error("Discount.QueryFailed", $"Failed to retrieve active discounts: {ex.Message}"));
        }
    }
}
