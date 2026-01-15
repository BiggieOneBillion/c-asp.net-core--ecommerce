using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.APPLICATION.Common.Models;
using Ecommerce.APPLICATION.Common.Security;
using MediatR;

namespace Ecommerce.APPLICATION.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehavior(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<HasPermissionAttribute>();

        if (authorizeAttributes.Any())
        {
            if (string.IsNullOrEmpty(_currentUserService.UserId))
            {
                return CreateFailureResponse("Unauthorized", "User is not authenticated.");
            }

            foreach (var attribute in authorizeAttributes)
            {
                if (!_currentUserService.HasPermission(attribute.Permission))
                {
                    return CreateFailureResponse("Forbidden", $"User does not have permission: {attribute.Permission}");
                }
            }
        }

        return await next();
    }

    private TResponse CreateFailureResponse(string code, string message)
    {
        var error = new Error(code, message);

        // Check if TResponse is Result<TValue>
        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var failureMethod = typeof(Result).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == "Failure" && m.IsGenericMethod)
                .MakeGenericMethod(resultType);

            return (failureMethod.Invoke(null, new object[] { error }) as TResponse)!;
        }

        // Check if TResponse is Result
        if (typeof(TResponse) == typeof(Result))
        {
            return (Result.Failure(error) as TResponse)!;
        }

        throw new System.Exception($"Request returns {typeof(TResponse).Name} which is not supported by AuthorizationBehavior. Use Result or Result<T>.");
    }
}
