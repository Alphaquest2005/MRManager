using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(IActorTerminated))]
    public class ActorTerminated : ProcessSystemMessage, IActorTerminated
    {
        
        public ActorTerminated() { }
        public IComplexEventService Actor { get; }

        public ActorTerminated(IComplexEventService actor, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo, process, source)
        {
            Actor = actor;
        }
    }
}
