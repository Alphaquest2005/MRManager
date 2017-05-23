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

[Export(typeof(IPersonPhoneNumberInfo))]
    public class PersonPhoneNumberInfo : EntityView<IPatients>, IPersonPhoneNumberInfo
    {
    public int PersonId { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; }
    }

    [Export(typeof(INextOfKinInfo))]
    public class NextOfKinInfo : EntityView<IPatients>, INextOfKinInfo
    {
        public int PatientId { get; set; }
        public string Relationship { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? MediaId { get; set; }
        public IList<IPersonAddressInfo> Addresses { get; set; }
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
    }

    [Export(typeof (IPersonAddressInfo))]
    public class PersonAddressInfo : EntityView<IPatients>, IPersonAddressInfo
    {
        public string AddressLines { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Parish { get; set; }
        public string State { get; set; }
        public string AddressType { get; set; }
    }
}