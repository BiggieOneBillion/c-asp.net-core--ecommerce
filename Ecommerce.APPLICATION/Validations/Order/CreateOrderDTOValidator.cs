using Ecommerce.APPLICATION.DTOs.Order;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Order
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order ID must be a valid GUID.");

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("User ID must be a valid GUID.");

            RuleFor(x => x.PaymentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Payment ID must be a valid GUID.");
        }
    }
}
