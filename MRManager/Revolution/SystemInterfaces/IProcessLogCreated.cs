using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    
    public interface IProcessLogCreated:IProcessSystemMessage
    {
        IEnumerable<IComplexEventLog> EventLogs { get; }
    }
}
