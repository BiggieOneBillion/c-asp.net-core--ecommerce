using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IUnitOfWork unitOfWork, IDiscountRepository discountRepository)
    {
        _unitOfWork = unitOfWork;
        _discountRepository = discountRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);

        if (discount == null)
            return Result.Failure<Unit>(new Error("Discount.NotFound", "Discount not found"));

        await _discountRepository.DeleteAsync(discount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
