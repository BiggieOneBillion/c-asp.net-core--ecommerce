using Ecommerce.APPLICATION.Common.Interfaces;

namespace Ecommerce.APPLICATION.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand;
