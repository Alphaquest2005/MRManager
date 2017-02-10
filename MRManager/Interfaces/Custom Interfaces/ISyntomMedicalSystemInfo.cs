using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface ISyntomMedicalSystemInfo : IEntityView<ISyntomMedicalSystems>
    {
        int MedicalSystemId { get; }
        string System { get;  }
        string SyntomName { get; }
        IList<IInterviewInfo> Interviews { get; }

    }
}