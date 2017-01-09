using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IComplexEventParameters
    {
        IComplexEventService Actor { get; }
        Dictionary<string,dynamic> Messages { get; }
       
    }
}