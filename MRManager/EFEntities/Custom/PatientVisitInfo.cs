using System;
using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public class PatientVisitInfo : EntityView<IPatientVisit>, IPatientVisitInfo
    {
        public int PatientId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string AttendingDoctor { get; set; }
        public IList<IPatientSyntomInfo> PatientSyntoms { get; set; }
    }
}