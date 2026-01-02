using System.Text.Json;
using Ecommerce.CORE.Common;
using Ecommerce.INFRASTRUCTURE.Data;
using Ecommerce.INFRASTRUCTURE.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.INFRASTRUCTURE.BackgroundJobs;

public class ProcessOutboxMessagesJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(
        ApplicationDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute()
    {
        _logger.LogInformation("Starting outbox message processing...");

        var messages = await _dbContext.OutboxMessages
            .Where(m => m.ProcessedOn == null)
            .OrderBy(m => m.OccurredOn)
            .Take(20)
            .ToListAsync();

        if (!messages.Any())
        {
            _logger.LogInformation("No unprocessed outbox messages found.");
            return;
        }

        foreach (var message in messages)
        {
            try
            {
                var domainEvent = DeserializeDomainEvent(message);

                _logger.LogInformation(
                    "Processing outbox message {MessageId} of type {EventType}",
                    message.Id,
                    message.Type);

                await _publisher.Publish(domainEvent);

                message.ProcessedOn = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error processing outbox message {MessageId}",
                    message.Id);

                message.Error = ex.ToString();
            }
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Finished outbox message processing.");
    }

    private IDomainEvent DeserializeDomainEvent(OutboxMessage message)
    {
        var type = Type.GetType(message.Type);

        if (type == null)
            throw new InvalidOperationException($"Type {message.Type} not found");

        var domainEvent = JsonSerializer.Deserialize(message.Content, type);

        if (domainEvent == null)
            throw new InvalidOperationException($"Failed to deserialize {message.Type}");

        return (IDomainEvent)domainEvent;
    }
}
