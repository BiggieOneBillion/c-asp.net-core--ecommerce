using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Admin.Commands.CreateDiscount;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result<GeneralResponse<Guid>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Guid>>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<CORE.Enums.DiscountType>(request.Type, true, out var type))
        {
            return Result.Failure<GeneralResponse<Guid>>(new Error("Discount.InvalidType", "Invalid discount type"));
        }

        if (!Enum.TryParse<CORE.Enums.DiscountScope>(request.Scope, true, out var scope))
        {
            return Result.Failure<GeneralResponse<Guid>>(new Error("Discount.InvalidScope", "Invalid discount scope"));
        }

        var discount = Discount.Create(
            name: request.Name,
            description: request.Description,
            code: request.CouponCode,
            type: type,
            value: request.Value,
            scope: scope,
            targetId: request.ApplicableProductIds?.FirstOrDefault() ?? request.ApplicableCategoryIds?.FirstOrDefault(),
            startDate: request.StartDate,
            endDate: request.EndDate,
            minimumOrderAmount: request.MinimumOrderAmount,
            usageLimit: request.UsageLimit
        );

        await _discountRepository.CreateAsync(discount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GeneralResponse<Guid>>.Success(GeneralResponse<Guid>.CreateSuccess(discount.Id, "Discount created successfully", 201));
    }
}
