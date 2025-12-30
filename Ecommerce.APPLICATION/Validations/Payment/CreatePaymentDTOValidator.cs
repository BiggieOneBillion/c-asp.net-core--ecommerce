using Ecommerce.APPLICATION.DTOs.Payment;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Payment
{
    public class CreatePaymentDTOValidator : AbstractValidator<CreatePaymentDTO>
    {
        public CreatePaymentDTOValidator()
        {
            RuleFor(x => x.PaymentType)
                .IsInEnum()
                .WithMessage("Payment type must be a valid value.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.PaymentDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Payment date cannot be in the future.");

            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order ID must be a valid GUID.");
        }
    }
}
