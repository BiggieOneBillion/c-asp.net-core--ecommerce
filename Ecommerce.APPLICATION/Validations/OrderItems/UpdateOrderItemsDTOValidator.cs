using Ecommerce.APPLICATION.DTOs.Order;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.OrderItems
{
    public class UpdateOrderItemsDTOValidator : AbstractValidator<UpdateOrderItemsDTO>
    {
        public UpdateOrderItemsDTOValidator()
        {
            RuleFor(x => x.OrderItems)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Item ID must be a valid GUID.");

            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order ID must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.CreateAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Create date cannot be in the future.");
        }
    }
}
