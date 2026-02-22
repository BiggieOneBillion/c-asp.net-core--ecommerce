using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Result<GeneralResponse<Unit>>>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralResponse<Unit>>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetByIdAsync(request.Id);
        if (discount == null)
        {
            return Result.Failure<GeneralResponse<Unit>>(new Error("Discount.NotFound", $"Discount with ID {request.Id} not found"));
        }

        await _discountRepository.DeleteAsync(discount);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GeneralResponse<Unit>>.Success(GeneralResponse<Unit>.CreateSuccess(Unit.Value, "Discount deleted successfully"));
    }
}
