using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface IPatientInfo : IEntityView<IPersons_Patient>
    {
        String Name { get; }
        String Address { get; }
        String PhoneNumber { get; }
        Int32? Age { get; }
        String Sex { get; }
        String BirthCountry { get; }
        String Email { get; }
        Int32? MediaId { get; }
    }
}