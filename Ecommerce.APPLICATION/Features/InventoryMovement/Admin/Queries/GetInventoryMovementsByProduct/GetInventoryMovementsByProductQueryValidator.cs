using FluentValidation;

namespace Ecommerce.APPLICATION.Features.InventoryMovement.Admin.Queries.GetInventoryMovementsByProduct;

public class GetInventoryMovementsByProductQueryValidator : AbstractValidator<GetInventoryMovementsByProductQuery>
{
    public GetInventoryMovementsByProductQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");
    }
}
