using Ecommerce.APPLICATION.DTOs.Order;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.Order
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemRequestDTOValidator());
        }
    }

    public class OrderItemRequestDTOValidator : AbstractValidator<OrderItemRequestDTO>
    {
        public OrderItemRequestDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
