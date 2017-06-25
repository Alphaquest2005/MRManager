using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPersonPhoneNumberInfo : IEntityView<IPatients>
    {
        Int32 PersonId { get; }
        string PhoneNumber { get; }
        String PhoneType { get;  }
        
    }
}