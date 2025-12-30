using Ecommerce.APPLICATION.Common.Models;
using MediatR;

namespace Ecommerce.APPLICATION.Common.Interfaces;

/// <summary>
/// Marker interface for queries that return a value.
/// </summary>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
