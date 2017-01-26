using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IPersonPhoneNumberInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; }
        string PhoneNumber { get; }
        String Type { get;  }
    }

    [InheritedExport]
    public partial interface IForeignPhoneNumberInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; }
        string PhoneNumber { get; }
        String Type { get; }
    }
}