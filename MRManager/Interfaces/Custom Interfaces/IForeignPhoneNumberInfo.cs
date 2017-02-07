using System;
using SystemInterfaces;

namespace Interfaces
{
    public partial interface IForeignPhoneNumberInfo : IEntityView<IPersons_Patient>
    {
        Int32 PersonId { get; }
        string PhoneNumber { get; }
        String Type { get; }
    }
}