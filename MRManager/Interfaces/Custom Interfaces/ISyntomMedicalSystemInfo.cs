using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface ISyntomMedicalSystemInfo : IEntityView<ISyntomMedicalSystems>
    {
        int MedicalSystemId { get; set; }
        string System { get; set; }
        IList<IInterviewInfo> Interviews { get; set; }
    }
}