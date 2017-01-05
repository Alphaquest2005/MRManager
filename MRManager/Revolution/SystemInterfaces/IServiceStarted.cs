using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces
{
    public interface IServiceStarted<out TService> : IProcessSystemMessage
    {
        TService Service { get; }
    }
    
}
