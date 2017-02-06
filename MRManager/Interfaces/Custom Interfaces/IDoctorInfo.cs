using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
    
    public interface IDoctorInfo : IEntityView<IPersons_Doctor>
    {
        string Name { get; }
        string Code { get; }
    }
}