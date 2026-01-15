using Ecommerce.APPLICATION.Common.Models;
using FluentValidation;
using MediatR;

namespace Ecommerce.APPLICATION.Common.Behaviors;

/// <summary>
/// Pipeline behavior that validates requests using FluentValidation.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));
            var error = new Error("Validation.Failed", errorMessage);

            // Handle both Result and Result<T> types
            var resultType = typeof(TResponse);
            if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var valueType = resultType.GetGenericArguments()[0];
                var genericFailureMethod = typeof(Result)
                    .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                    .First(m => m.Name == "Failure" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 1)
                    .MakeGenericMethod(valueType);
                return (TResponse)genericFailureMethod.Invoke(null, new object[] { error })!;
            }

            return (TResponse)(object)Result.Failure(error);
        }

        return await next();
    }
}
