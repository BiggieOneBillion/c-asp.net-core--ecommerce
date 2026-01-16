using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.UpdateDiscount;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountRepository _discountRepository;

    public UpdateDiscountCommandHandler(IUnitOfWork unitOfWork, IDiscountRepository discountRepository)
    {
        _unitOfWork = unitOfWork;
        _discountRepository = discountRepository;
    }

    public async Task<Result<Unit>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);

        if (discount == null)
            return Result.Failure<Unit>(new Error("Discount.NotFound", "Discount not found"));

        discount.Name = request.Name;
        discount.Description = request.Description;
        discount.IsActive = request.IsActive;

        await _discountRepository.UpdateAsync(discount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
