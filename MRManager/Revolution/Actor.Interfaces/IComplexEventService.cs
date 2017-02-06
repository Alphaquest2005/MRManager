using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    
    public interface IComplexEventService: IService<IComplexEventService>
    {
        string ActorId { get; }
        IComplexEventAction ComplexEventAction { get; }
        ISystemProcess Process { get; }
    }
}
