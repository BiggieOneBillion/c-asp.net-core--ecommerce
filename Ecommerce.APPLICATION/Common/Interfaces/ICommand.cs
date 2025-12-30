using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Common.Interfaces;

/// <summary>
/// Marker interface for commands that don't return a value.
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Marker interface for commands that return a value.
/// </summary>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
