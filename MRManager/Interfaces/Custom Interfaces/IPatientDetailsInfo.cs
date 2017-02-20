using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPatientDetailsInfo : IEntityView<IPatients>
    {
        String Name { get; }
        string IdNumber { get; }
        Int32? Age { get; }
        String Address { get; }
        String PhoneNumber { get; }
        String Allergies { get; }
        String BirthCountry { get; }
        String Job { get; }
        String Religion { get; }
        String Sex { get; }
        String EmailAddress { get; }
        String MaritalStatus { get; }
        String CountryOfResidence { get; }
        Int32? MediaId { get; }

        //IList<IPersonAddressInfo> Addresses { get; }
        //IList<IPersonPhoneNumberInfo> PhoneNumbers { get; }

        //INonResidentInfo NonResident { get; }
        //IList<INextOfKinInfo> NextOfKins { get; }
        
        DateTime BirthDate { get; }
        
    }

    public interface IPatientAddressesInfo : IEntityView<IPatients>
    {
        IList<IPersonAddressInfo> Addresses { get; }
    }

    public interface IPatientNextOfKinsInfo : IEntityView<IPatients>
    {
        IList<INextOfKinInfo> NextOfKins { get; }
    }

    public interface IPatientPhoneNumbersInfo : IEntityView<IPatients>
    {
        IList<IPersonPhoneNumberInfo> PhoneNumbers { get; }
    }
}