using Ecommerce.APPLICATION.DTOs.InventoryMovement;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.InventoryMovement
{
    public class CreateInventoryMovementValidator : AbstractValidator<CreateInventoryMovement>
    {
        public CreateInventoryMovementValidator()
        {
            RuleFor(x => x.InventoryMovementId)
                .NotEqual(Guid.Empty)
                .WithMessage("Inventory Movement ID must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.QuantityChanged)
                .NotEqual(0)
                .WithMessage("Quantity changed must not be 0.");

            RuleFor(x => x.MovementType)
                .IsInEnum()
                .WithMessage("Movement type must be a valid value.");

            RuleFor(x => x.Reason)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Reason))
                .WithMessage("Reason must not exceed 500 characters.");
        }
    }
}
