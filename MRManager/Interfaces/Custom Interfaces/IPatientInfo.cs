using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface IPatientInfo : IEntityView<IPatients>
    {
        String Name { get; }
        String Address { get; }
        String PhoneNumber { get; }
        string Age { get; }
        String Sex { get; }
        String BirthCountry { get; }
        String Email { get; }
        Int32? MediaId { get; }
        DateTime BirthDate { get; }
    }
}