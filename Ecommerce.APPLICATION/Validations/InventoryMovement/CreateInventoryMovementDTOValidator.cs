using Ecommerce.APPLICATION.DTOs.InventoryMovement;
using FluentValidation;

namespace Ecommerce.APPLICATION.Validations.InventoryMovement
{
    public class CreateInventoryMovementDTOValidator : AbstractValidator<CreateInventoryMovementDTO>
    {
        public CreateInventoryMovementDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty).WithMessage("Product ID must be a valid GUID.");

            RuleFor(x => x.QuantityChanged)
                .NotEqual(0).WithMessage("Quantity changed cannot be zero.");

            RuleFor(x => x.MovementType)
                .IsInEnum().WithMessage("Movement type must be a valid value.");
        }
    }
}
