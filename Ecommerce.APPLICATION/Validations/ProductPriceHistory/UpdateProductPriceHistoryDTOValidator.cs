using Ecommerce.APPLICATION.DTOs.ProductPriceHistory;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.ProductPriceHistory
{
    public class UpdateProductPriceHistoryDTOValidator : AbstractValidator<UpdateProductPriceHistoryDTO>
    {
        public UpdateProductPriceHistoryDTOValidator()
        {
            RuleFor(x => x.ProductPriceHistoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Price History ID must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.NewPrice)
                .GreaterThan(0)
                .WithMessage("New price must be greater than 0.");

            RuleFor(x => x.OldPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Old price must be greater than or equal to 0.");

            RuleFor(x => x.EffectiveDate)
                .NotEmpty()
                .WithMessage("Effective date is required.");

            RuleFor(x => x.ChangedAt)
                .NotEmpty()
                .WithMessage("Changed at date is required.");
        }
    }
}
