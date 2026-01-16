using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.CORE.Common;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.CreateDiscount;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscountRepository _discountRepository;

    public CreateDiscountCommandHandler(IUnitOfWork unitOfWork, IDiscountRepository discountRepository)
    {
        _unitOfWork = unitOfWork;
        _discountRepository = discountRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var discount = new Discount
            {
                Name = request.Name,
                Description = request.Description,
                Code = request.Code,
                Type = request.Type,
                Value = request.Value,
                Scope = request.Scope,
                TargetId = request.TargetId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MinimumOrderAmount = request.MinimumOrderAmount,
                UsageLimit = request.UsageLimit,
                IsActive = true
            };

            await _discountRepository.CreateAsync(discount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(discount.Id);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(new Error("Discount.CreateFailed", $"Failed to create discount: {ex.Message}"));
        }
    }
}
