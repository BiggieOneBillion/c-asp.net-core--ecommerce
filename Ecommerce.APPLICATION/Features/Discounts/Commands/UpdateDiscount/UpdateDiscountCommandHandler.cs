using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);
        if (discount == null)
        {
            return Result.Failure<GeneralResponse<Unit>>(new Error("Discount.NotFound", $"Discount with ID {request.Id} not found"));
        }

        discount.Name = request.Name;
        discount.Description = request.Description;
        discount.IsActive = request.IsActive;

        await _discountRepository.UpdateAsync(discount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Discount updated successfully"));
    }
}
