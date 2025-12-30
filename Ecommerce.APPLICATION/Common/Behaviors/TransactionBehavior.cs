using Ecommerce.APPLICATION.Common.Interfaces;
using Ecommerce.CORE.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.Behaviors;

/// <summary>
/// Pipeline behavior that wraps command execution in a database unit of work.
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(
        IUnitOfWork unitOfWork,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only apply UoW for commands, not queries
        if (request is not ICommand && request is not ICommand<TResponse>)
        {
            return await next();
        }

        var requestName = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("Executing command {RequestName}", requestName);

            var response = await next();

            _logger.LogInformation("Committing changes for {RequestName}", requestName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully executed and committed command {RequestName}", requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred during command execution for {RequestName}. Changes not committed.",
                requestName);

            throw;
        }
    }
}
