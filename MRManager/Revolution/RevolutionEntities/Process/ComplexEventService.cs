using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Actor.Interfaces;

namespace RevolutionEntities.Process
{
    public class ComplexEventService : IComplexEventService
    {
        public ComplexEventService(string actorId, IComplexEventAction complexEventAction, ISystemProcess process, ISystemSource source)
        {
            ComplexEventAction = complexEventAction;
            Process = process;
            Source = source;
            ActorId = actorId;
        }

        public ISystemSource Source { get; }
        public string ActorId { get; }
        public IComplexEventAction ComplexEventAction { get; }
        public ISystemProcess Process { get; }
    }
}
