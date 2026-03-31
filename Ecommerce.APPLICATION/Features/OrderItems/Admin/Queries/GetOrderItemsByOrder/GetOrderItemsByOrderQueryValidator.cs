using FluentValidation;

namespace Ecommerce.APPLICATION.Features.OrderItems.Admin.Queries.GetOrderItemsByOrder;

public class GetOrderItemsByOrderQueryValidator : AbstractValidator<GetOrderItemsByOrderQuery>
{
    public GetOrderItemsByOrderQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required.");
    }
}
