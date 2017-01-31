using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface IPatientSyntomInfo : IEntityView<IPatientSyntoms>
    {
        string Syntom { get; set; }
        int Priority { get; set; }
        string Status { get; set; }
        IList<ISyntomMedicalSystemInfo> Systems { get; set; }
    }
}