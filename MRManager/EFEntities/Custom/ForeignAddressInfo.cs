using System.ComponentModel.Composition;
using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
    [Export(typeof(IForeignAddressInfo))]
    public class ForeignAddressInfo : EntityView<IPersons_Patient>, IForeignAddressInfo
    {
        
        public string Address { get; set; }
        public string Addresslines { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Parish { get; set; }
        public string State { get; set; }
        public string Addresstype { get; set; }
        public string ZipOrPostalCode { get; set; }
        public string AddressTypeName { get; set; }
        public int PersonId { get; set; }
    }
}