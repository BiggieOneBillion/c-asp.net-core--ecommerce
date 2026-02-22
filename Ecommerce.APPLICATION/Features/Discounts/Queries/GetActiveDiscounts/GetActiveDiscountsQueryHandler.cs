using AutoMapper;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Queries.GetActiveDiscounts;

public class GetActiveDiscountsQueryHandler : IRequestHandler<GetActiveDiscountsQuery, Result<List<DiscountResponseDTO>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetActiveDiscountsQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<DiscountResponseDTO>>> Handle(GetActiveDiscountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var activeDiscounts = await _discountRepository.GetActiveDiscountsAsync();

            if (activeDiscounts == null)
            {
                return Result.Failure<List<DiscountResponseDTO>>(new Error("Discount.QueryFailed", $"No active discounts"));
            }
            var discountDtos = _mapper.Map<List<DiscountResponseDTO>>(activeDiscounts);
            return Result.Success(discountDtos);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<DiscountResponseDTO>>(new Error("Discount.QueryFailed", $"Failed to retrieve active discounts: {ex.Message}"));
        }
    }
}
