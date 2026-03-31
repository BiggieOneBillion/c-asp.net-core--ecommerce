using FluentValidation;

namespace Ecommerce.APPLICATION.Features.ProductPriceHistory.Admin.Queries.GetPriceHistoryByProduct;

public class GetPriceHistoryByProductQueryValidator : AbstractValidator<GetPriceHistoryByProductQuery>
{
    public GetPriceHistoryByProductQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than zero.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");
    }
}
