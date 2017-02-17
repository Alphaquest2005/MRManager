using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IPersonAddressInfo))]
    public class PersonAddressInfo : EntityView<IPersons_Patient>, IPersonAddressInfo
    {
        public string AddressLines { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Parish { get; set; }
        public string State { get; set; }
        public string AddressType { get; set; }
    }
}