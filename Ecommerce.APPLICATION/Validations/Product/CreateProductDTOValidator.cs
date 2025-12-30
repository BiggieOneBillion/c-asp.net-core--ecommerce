using Ecommerce.APPLICATION.DTOs.Product;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Product
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(200)
                .WithMessage("Product name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description is required.")
                .MaximumLength(1000)
                .WithMessage("Product description must not exceed 1000 characters.");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Category ID must be a valid GUID.");

            RuleFor(x => x.CurrentPrice)
                .GreaterThan(0)
                .WithMessage("Current price must be greater than 0.");
        }
    }
}
