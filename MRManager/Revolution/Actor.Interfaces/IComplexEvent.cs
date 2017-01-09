using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IComplexEvent
    {
        string Key { get; }
        IList<IProcessExpectedEvent> Events { get; }
        int ProcessId { get; }
       

    }
}