using Ecommerce.CORE.Constants;
using Ecommerce.APPLICATION.Common.Security;
using Ecommerce.APPLICATION.Common.Interfaces;
using MediatR;

namespace Ecommerce.APPLICATION.Features.Discounts.Commands.DeleteDiscount;

[HasPermission(Permissions.Discounts.Delete)]
public record DeleteDiscountCommand(Guid Id) : ICommand<Unit>;
