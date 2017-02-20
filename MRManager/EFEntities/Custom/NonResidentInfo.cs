using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(INonResidentInfo))]
    public class NonResidentInfo : EntityView<IPatients>, INonResidentInfo
    {
        public string Type { get; set; }
        public string BoatName { get; set; }
        public string Marina { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string School { get; set; }
        public string HotelName { get; set; }
        public IList<IForeignAddressInfo> Addresses { get; set; }
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
    }
}