using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientNextOfKinsInfo))]
    public class PatientNextOfKinsInfo : EntityView<IPatients>, IPatientNextOfKinsInfo
    {
        
        public IList<INextOfKinInfo> NextOfKins { get; set; }
    }
}