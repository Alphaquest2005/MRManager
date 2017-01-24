using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IPersonPhoneNumberInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; }
        
        String Type { get;  }
    }
}