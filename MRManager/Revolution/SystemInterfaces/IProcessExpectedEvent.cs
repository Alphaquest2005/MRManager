using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    public interface IProcessExpectedEvent
    {
        Func<IProcessSystemMessage, bool> EventPredicate { get; }
        Type EventType { get; }
        int ProcessId { get; }
        
    }
}
