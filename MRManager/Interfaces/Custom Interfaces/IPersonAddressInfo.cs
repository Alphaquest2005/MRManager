using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    [InheritedExport]
    public interface IPersonAddressInfo : IEntityView<IPersons_Patient>
    {
        string City { get; set; }
        string Country { get; set; }
        string Parish { get; set; }
        string State { get; set; }
        string Addresstype { get; set; }
    }
}