using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    public class PersonAddressInfo : EntityView<IPersons_Patient>, IPersonAddressInfo
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Parish { get; set; }
        public string State { get; set; }
        public string Addresstype { get; set; }
    }
}