using FluentValidation;

namespace Ecommerce.APPLICATION.Features.Payments.Admin.Queries.GetPaymentsByOrder;

public class GetPaymentsByOrderQueryValidator : AbstractValidator<GetPaymentsByOrderQuery>
{
    public GetPaymentsByOrderQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required.");
    }
}
