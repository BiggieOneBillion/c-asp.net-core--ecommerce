using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.APPLICATION.Common.EventHandlers
{
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {

        private readonly ILogger<UserCreatedDomainEventHandler> _logger;

        public UserCreatedDomainEventHandler(
           ILogger<UserCreatedDomainEventHandler> logger
        )
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
             _logger.LogInformation(
            "Handling UserCreatedDomainEvent for User {UserId}.",
            notification.EventId);

            // Add your handling logic here

            return Task.CompletedTask;
        }
    }
}