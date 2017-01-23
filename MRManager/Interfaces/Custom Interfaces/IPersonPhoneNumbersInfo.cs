using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface IPersonPhoneNumbersInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; set; }
        String PhoneNumber { get; set; }
        String Type { get; set; }
    }
}