using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using RevolutionEntities.Process;

namespace RevolutionData.Context
{
    public class Domain
    {
        public class Commands
        {
            public static IStateCommand PublishDomainEvent => new StateCommand("PublishMessage", "Publish Message",Events.DomainEventPublished);
        }

        public class Events
        {
            public static IStateEvent DomainEventPublished => new StateEvent("DomainEventPublished", "Domain Event Published", "");
        }

    }
}
