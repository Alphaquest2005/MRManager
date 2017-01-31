using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface ISyntomMedicalSystemInfo : IEntityView<ISyntomMedicalSystems>
    {
        int MedicalSystemId { get; set; }
        string System { get; set; }
        IList<IInterviewInfo> Interviews { get; set; }
    }
}