using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;

namespace Interfaces
{
    public interface IPatientHistoryInfo : IEntityView<IPatients>
    {
        IPatientDetailsInfo PatientDetails { get; set; }
        List<IPatientVisitInfo> PatientVisits { get; set; }
        List<ISyntomInfo> Synptoms { get; set; }
    }
}
