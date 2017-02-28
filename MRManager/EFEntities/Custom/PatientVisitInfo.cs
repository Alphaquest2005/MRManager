using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IPatientVisitInfo))]
    public class PatientVisitInfo : EntityView<IPatientVisit>, IPatientVisitInfo
    {
        public int PatientId { get; set; }
        public DateTime DateOfVisit { get; set; } = DateTime.Today;
        public string AttendingDoctor { get; set; }
        public int DoctorId { get; set; }
        public IList<IPatientSyntomInfo> PatientSyntoms { get; set; }
        public string Purpose { get; set; }
        public int VisitTypeId { get; set; }
    }
}