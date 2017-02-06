using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface INextOfKinInfo : IEntityView<IPersons_Patient>
    {
        Int32 PatientId { get; }
        String Relationship { get; }
        String Name { get; }
        String Address { get; }
        String PhoneNumber { get; }
        String Email { get; }
        Int32? MediaId { get; }

        IList<IPersonAddressInfo> Addresses { get; }
        IList<IPersonPhoneNumberInfo> PhoneNumbers { get; }
    }
}