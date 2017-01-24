using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public partial class PatientDetailsInfo : EntityView<IPersons_Patient>, IPatientDetailsInfo
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Allergies { get; set; }
        public string BirthCountry { get; set; }
        public string Job { get; set; }
        public string Religion { get; set; }
        public string Sex { get; set; }
        public string EmailAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string CountryOfResidence { get; set; }
        public int? MediaId { get; set; }
        public IList<IPersonAddressInfo> Addresses { get; set; }
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
        public INonResidentInfo NonResident { get; set; }
        public IList<INextOfKinInfo> NextOfKins { get; set; }
    }


}