using Ecommerce.APPLICATION.DTOs.Product;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Product
{
    public class UpdateProductPriceDTOValidator : AbstractValidator<UpdateProductPriceDTO>
    {
        public UpdateProductPriceDTOValidator()
        {
            RuleFor(x => x.NewPrice)
                .GreaterThan(0).WithMessage("New price must be greater than zero.");
        }
    }
}
