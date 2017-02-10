using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;
using ReactiveUI;

namespace EF.Entities
{
    [Export(typeof(IPatientSyntomInfo))]
    public class PatientSyntomInfo : EntityView<IPatientSyntoms>, IPatientSyntomInfo
    {
        public int SyntomId { get; set; }
        public string SyntomName { get; set; }
        public ISyntoms Syntom { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int PatientVisitId { get; set; }

    }
}