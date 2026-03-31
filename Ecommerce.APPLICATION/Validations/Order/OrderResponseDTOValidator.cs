using Ecommerce.APPLICATION.DTOs.Order;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Order
{
    public class OrderResponseDTOValidator : AbstractValidator<OrderResponseDTO>
    {
        public OrderResponseDTOValidator()
        {
            RuleFor(x => x.OrderId).NotEqual(Guid.Empty);
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.PaymentId).NotEqual(Guid.Empty);
        }
    }
}
