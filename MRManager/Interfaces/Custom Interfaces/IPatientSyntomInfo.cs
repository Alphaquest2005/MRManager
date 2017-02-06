using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPatientSyntomInfo : IEntityView<IPatientSyntoms>
    {
        string Syntom { get; set; }
        string Priority { get; set; }
        string Status { get; set; }
        int PriorityId { get; set; }
        int StatusId { get; set; }
        IList<ISyntomMedicalSystemInfo> Systems { get; set; }
    }
}