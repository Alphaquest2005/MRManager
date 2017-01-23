using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface INextOfKinInfo : IEntityView<IPersons_Patient>
    {
        Int32 PatientId { get; }
        String Relationship { get; }
        String PersonName { get; }
        String Address { get; }
        String PhoneNumber { get; }
        String Email { get; }
        Int32? MediaId { get; }

        IList<IPersonAddressInfo> PersonAddressInfoes { get; }
        IList<IPersonPhoneNumberInfo> PersonPhoneNumberInfoes { get; }
    }
}