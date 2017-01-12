using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Events
{


    public class ActorTerminated : ProcessSystemMessage, IActorTerminated
    {
        public IComplexEventService Actor { get; }

        public ActorTerminated(IComplexEventService actor, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo, process, source)
        {
            Actor = actor;
        }
    }
}
