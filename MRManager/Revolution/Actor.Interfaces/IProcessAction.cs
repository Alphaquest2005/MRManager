using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IProcessAction
    {
        Func<IComplexEventParameters, IProcessSystemMessage> Action { get; set; }
        Func<IComplexEventParameters, IProcessStateInfo> ProcessInfo { get; set; }
        ISourceType ExpectedSourceType { get; set; }
    }
}
