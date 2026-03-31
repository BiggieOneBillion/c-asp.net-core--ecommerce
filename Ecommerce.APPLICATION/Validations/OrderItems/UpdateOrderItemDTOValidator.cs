using Ecommerce.APPLICATION.DTOs.OrderItems;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.OrderItems
{
    public class UpdateOrderItemDTOValidator : AbstractValidator<UpdateOrderItemDTO>
    {
        public UpdateOrderItemDTOValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty).WithMessage("Order ID must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty).WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
                
            RuleFor(x => x.CreateAt)
                .NotEmpty().WithMessage("CreateAt is required.");
        }
    }
}
