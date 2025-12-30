using Ecommerce.APPLICATION.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.Behaviors;

/// <summary>
/// Pipeline behavior that wraps command execution in a database transaction.
/// Note: This is a placeholder for future transaction support.
/// Actual transaction handling should be implemented when IUnitOfWork is available.
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only apply transaction for commands, not queries
        if (request is not ICommand && request is not ICommand<TResponse>)
        {
            return await next();
        }

        var requestName = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("Executing command {RequestName}", requestName);

            var response = await next();

            _logger.LogInformation("Successfully executed command {RequestName}", requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred during command execution for {RequestName}",
                requestName);

            throw;
        }
    }
}
