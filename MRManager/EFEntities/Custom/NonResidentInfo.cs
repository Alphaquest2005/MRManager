using System;
using System.Collections.Generic;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public class NonResidentInfo : EntityView<IPersons_Patient>, INonResidentInfo
    {
        public string Type { get; set; }
        public string BoatName { get; set; }
        public string MarinaList { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string School { get; set; }
        public string HotelName { get; set; }
        public IList<IForeignAddressInfo> Addresses { get; set; }
        public IList<IPersonPhoneNumberInfo> PhoneNumbers { get; set; }
    }
}