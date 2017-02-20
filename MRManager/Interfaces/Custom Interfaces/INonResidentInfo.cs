using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public partial interface INonResidentInfo : IEntityView<IPatients>
    {
        String Type { get; }
        String BoatName { get; }
        String Marina { get; }
        DateTime? ArrivalDate { get; }
        DateTime? DepartureDate { get; }
        String School { get; }
        String HotelName { get; }

        IList<IForeignAddressInfo> Addresses { get; }
        IList<IPersonPhoneNumberInfo> PhoneNumbers { get; }
    }
}