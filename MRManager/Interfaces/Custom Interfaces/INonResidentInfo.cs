using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public partial interface INonResidentInfo : IEntity //  MREntitiesQS|NonResidentPatientInfo
    {
        String Type { get; }
        String BoatName { get; }
        String MarinaList { get; }
        DateTime? ArrivalDate { get; }
        DateTime? DepartureDate { get; }
        String School { get; }
        String HotelName { get; }

        IList<IPersonAddressInfo> ForeignAddressInfoes { get; }
        IList<IPersonPhoneNumberInfo> ForeignPhoneNumberInfoes { get; }
    }
}