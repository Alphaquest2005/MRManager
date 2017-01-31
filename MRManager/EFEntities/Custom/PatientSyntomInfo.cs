using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public class PatientSyntomInfo : EntityView<IPatientSyntoms>, IPatientSyntomInfo
    {
        public string Syntom { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public IList<ISyntomMedicalSystemInfo> Systems { get; set; }
    }
}