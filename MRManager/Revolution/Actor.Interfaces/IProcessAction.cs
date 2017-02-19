using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    
    public interface IProcessAction
    {
        Func<IComplexEventParameters, Task<IProcessSystemMessage>> Action { get; set; }
        Func<IComplexEventParameters, IProcessStateInfo> ProcessInfo { get; set; }
        ISourceType ExpectedSourceType { get; set; }
    }
}
