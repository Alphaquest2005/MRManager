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
        public ComplexEventService(IComplexEventAction complexEventAction, ISystemProcess process, ISystemSource source)
        {
            ComplexEventAction = complexEventAction;
            Process = process;
            Source = source;
        }

        public ISystemSource Source { get; }
        public IComplexEventAction ComplexEventAction { get; }
        public ISystemProcess Process { get; }
    }
}
