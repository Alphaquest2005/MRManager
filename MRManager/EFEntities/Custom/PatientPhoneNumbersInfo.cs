using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPatientPhoneNumbersInfo))]
    public class PatientPhoneNumbersInfo: EntityView<IPatients>, IPatientPhoneNumbersInfo
    {
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
    }
}