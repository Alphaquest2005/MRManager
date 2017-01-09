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
    public interface IComplexEventAction : IComplexEvent
    {
        Type ExpectedMessageType { get; }
        IProcessStateDetailedInfo ProcessInfo { get; }
        IProcessAction Action { get; }
    }
}
