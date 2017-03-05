using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using CommonMessages;
using Actor.Interfaces;

namespace EventMessages.Commands
{

    [Export(typeof(ICreateProcessActor))]
    public class CreateProcessActor:ProcessSystemMessage, ICreateProcessActor
    {
        public CreateProcessActor(){}

        public CreateProcessActor(IList<IComplexEventAction> complexEvents, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source):base(processInfo,process, source)
        {
            ComplexEvents = complexEvents;
        }

        public IList<IComplexEventAction> ComplexEvents { get; }
    }
}
