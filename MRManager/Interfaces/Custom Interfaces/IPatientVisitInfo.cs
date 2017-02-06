using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPatientVisitInfo : IEntityView<IPatientVisit>
    {
        int PatientId { get; set; }
        DateTime DateOfVisit { get; set; }
        string AttendingDoctor { get; set; }
        int DoctorId { get; set; }
        IList<IPatientSyntomInfo> PatientSyntoms { get; set; }
        string Purpose { get; set; }
        int VisitTypeId { get; }
    }
}