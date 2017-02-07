using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPersonPhoneNumberInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; }
        string PhoneNumber { get; }
        String Type { get;  }
    }
}