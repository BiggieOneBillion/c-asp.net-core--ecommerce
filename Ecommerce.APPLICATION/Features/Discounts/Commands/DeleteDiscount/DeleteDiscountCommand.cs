using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;

[HasPermission(Permissions.Users.View)] // Placeholder
public record DeleteDiscountCommand(Guid Id) : ICommand<Unit>;
