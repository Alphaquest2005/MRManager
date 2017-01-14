using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Actor.Interfaces
{
    [InheritedExport]
    public interface IComplexEventParameters
    {
        IComplexEventService Actor { get; }
        ImmutableDictionary<string, dynamic> Messages { get; }
       
    }
}