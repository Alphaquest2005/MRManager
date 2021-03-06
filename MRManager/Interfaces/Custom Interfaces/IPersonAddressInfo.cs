using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IPersonAddressInfo : IEntityView<IPatients>
    {
        string Address { get; }
        string City { get;  }
        string Country { get;  }
        string Parish { get; }
        string State { get;  }
        string AddressType { get; }
        int PersonId { get; }
    }
}