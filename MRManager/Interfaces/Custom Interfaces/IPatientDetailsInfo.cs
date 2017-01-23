using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IPatientDetailsInfo : IEntityView<IPersons_Patient>
    {
        String Name { get; }
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

        IList<IPersonAddressInfo> PersonAddressInfoes { get; }
        IList<IPersonPhoneNumberInfo> PersonPhoneNumberInfoes { get; }
    }
}