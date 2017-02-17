using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPersonAddressInfo : IEntityView<IPersons_Patient>
    {
        string AddressLines { get; }
        string City { get;  }
        string Country { get;  }
        string Parish { get; }
        string State { get;  }
        string AddressType { get; }
    }
}