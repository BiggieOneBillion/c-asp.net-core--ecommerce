using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Common;

namespace Ecommerce.CORE.DomainEvents
{
    public class UserCreatedDomainEvent: DomainEvent
    {
        public Guid userId { get; set; }

         public override string EventType()
        {
            return DomainEventTypes.UserCreated;
        }

        public UserCreatedDomainEvent(Guid userId)
        {
            this.userId = userId;
        }
    }
}