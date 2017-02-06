using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    
    public interface ICreateEntityViewService : IProcessSystemMessage
    {
        Type ActorType { get; }
        object Action { get; }
    }
}
