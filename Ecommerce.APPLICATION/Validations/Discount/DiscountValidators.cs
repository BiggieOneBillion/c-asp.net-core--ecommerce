using Ecommerce.APPLICATION.DTOs.Discount;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Discount
{
    public class CreateDiscountDTOValidator : AbstractValidator<CreateDiscountDTO>
    {
        public CreateDiscountDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Discount name is required.")
                .MaximumLength(100).WithMessage("Discount name must not exceed 100 characters.");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Discount value must be greater than zero.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Discount type is required.")
                .Must(type => type == "Percentage" || type == "FixedAmount")
                .WithMessage("Discount type must be either 'Percentage' or 'FixedAmount'.");

            RuleFor(x => x.Scope)
                .NotEmpty().WithMessage("Discount scope is required.")
                .Must(scope => scope == "Global" || scope == "Category" || scope == "Product")
                .WithMessage("Discount scope must be 'Global', 'Category', or 'Product'.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
        }
    }

    public class UpdateDiscountDTOValidator : AbstractValidator<UpdateDiscountDTO>
    {
        public UpdateDiscountDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Discount name is required.")
                .MaximumLength(100).WithMessage("Discount name must not exceed 100 characters.");
        }
    }

    public class ValidateCouponDTOValidator : AbstractValidator<ValidateCouponDTO>
    {
        public ValidateCouponDTOValidator()
        {
            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("Coupon code is required.");

            RuleFor(x => x.OrderTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Order total must be greater than or equal to zero.");
        }
    }
}
